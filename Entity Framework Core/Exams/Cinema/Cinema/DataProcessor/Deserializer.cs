namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Cinema.Data.Models.Enums;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie 
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat 
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection 
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket 
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var moviesDtos = JsonConvert.DeserializeObject<ImportMovieDto[]>(jsonString);
            var resultSb = new StringBuilder();
            var validMovies = new List<Movie>();

            foreach (var movieDto in moviesDtos)
            {
                if (!IsValid(movieDto) ||
                    context.Movies.Any(m => m.Title == movieDto.Title))
                {
                    resultSb.AppendLine(ErrorMessage);
                    continue;
                }

                var movie = new Movie()
                {
                    Title = movieDto.Title,
                    Rating = movieDto.Rating,
                    Director = movieDto.Director,
                    Duration = TimeSpan.Parse(movieDto.Duration),
                    Genre = (Genre)Enum.Parse(typeof(Genre), movieDto.Genre),
                };

                validMovies.Add(movie);
                resultSb.AppendLine(string.Format(SuccessfulImportMovie,
                                    movieDto.Title,
                                    movieDto.Genre,
                                    movieDto.Rating.ToString("F2")));
            }

            context.Movies.AddRange(validMovies);
            context.SaveChanges();

            var result = resultSb.ToString().TrimEnd();
            return result;
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var hallsDtos = JsonConvert.DeserializeObject<ImportHallSeatsDto[]>(jsonString);

            var resultSb = new StringBuilder();
            var validHalls = new List<Hall>();

            foreach (var hallDto in hallsDtos)
            {
                if (!IsValid(hallDto))
                {
                    resultSb.AppendLine(ErrorMessage);
                    continue;
                }

                var hall = new Hall()
                {
                    Name = hallDto.Name,
                    Is4Dx = hallDto.Is4Dx,
                    Is3D = hallDto.Is3D
                };

                AddHallSeats(hall, hallDto.Seats);
                validHalls.Add(hall);

                var projectionType = GetProjectionType(hall);
                resultSb.AppendLine(string.Format(SuccessfulImportHallSeat,
                                    hall.Name, projectionType, hall.Seats.Count));
            }

            context.Halls.AddRange(validHalls);
            context.SaveChanges();

            var result = resultSb.ToString().TrimEnd();
            return result;
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportProjectionDto[]),
                                                  new XmlRootAttribute("Projections"));
            var reader = new StringReader(xmlString);
            var projectionDtos = (ImportProjectionDto[])xmlSerializer.Deserialize(reader);

            var resultSb = new StringBuilder();
            var validProjections = new List<Projection>();

            foreach (var projectionDto in projectionDtos)
            {
                if (!IsValid(projectionDto) ||
                    !IsMovieValid(projectionDto.MovieId, context) ||
                    !IsHallValid(projectionDto.HallId, context))
                {
                    resultSb.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = new Projection()
                {
                    MovieId = projectionDto.MovieId,
                    HallId = projectionDto.HallId,
                    DateTime = DateTime.ParseExact(projectionDto.DateTime,
                                                   "yyyy-MM-dd HH:mm:ss",
                                                   CultureInfo.InvariantCulture)
                };

                validProjections.Add(projection);
                context.Projections.Add(projection);
                context.SaveChanges();

                resultSb.AppendLine(String.Format(SuccessfulImportProjection,
                                                  projection.Movie.Title,
                                                  projection.DateTime.ToString("MM/dd/yyyy")));
            }

            var result = resultSb.ToString().TrimEnd();
            return result;
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportCustomerTicketsDto[]),
                                                  new XmlRootAttribute("Customers"));

            var reader = new StringReader(xmlString);
            var customerDtos = (ImportCustomerTicketsDto[])xmlSerializer.Deserialize(reader);
            var resultSb = new StringBuilder();

            foreach (var customerDto in customerDtos)
            {
                if (!IsValid(customerDto))
                {
                    resultSb.AppendLine(ErrorMessage);
                    continue;
                }

                var customerTickets = new List<Ticket>();

                foreach (var ticketDto in customerDto.Tickets)
                {
                    var ticket = new Ticket()
                    {
                        ProjectionId = ticketDto.ProjectionId,
                        Price = ticketDto.Price
                    };

                    customerTickets.Add(ticket);
                }

                var customer = new Customer()
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    Age = customerDto.Age,
                    Balance = customerDto.Balance,
                    Tickets = customerTickets
                };

                context.Customers.Add(customer);
                context.SaveChanges();

                resultSb.AppendLine(String.Format(SuccessfulImportCustomerTicket,
                                                  customerDto.FirstName,
                                                  customerDto.LastName,
                                                  customerDto.Tickets.Count()));
            }

            var result = resultSb.ToString().TrimEnd();
            return result;
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity,
                                                       validationContext,
                                                       validationResult,
                                                       true);

            return isValid;
        }

        // Import Halls and Seats Helper Function
        private static string GetProjectionType(Hall hall)
        {
            if (hall.Is3D && hall.Is4Dx)
                return "4Dx/3D";
            else if (hall.Is4Dx)
                return "4Dx";
            else if (hall.Is3D)
                return "3D";
            else
                return "Normal";
        }

        // Import Halls and Seats Helper Function
        private static void AddHallSeats(Hall hall, int seatsCount)
        {
            for (int i = 0; i < seatsCount; i++)
            {
                var newSeat = new Seat();
                hall.Seats.Add(newSeat);
            }
        }

        // Import Projections Dto Validation Helper Method
        private static bool IsHallValid(int hallId, CinemaContext context)
        {
            var isValid = context.Halls.Any(h => h.Id == hallId);
            return isValid;
        }

        // Import Projections Dto Validation Helper Method
        private static bool IsMovieValid(int movieId, CinemaContext context)
        {
            var isValid = context.Movies.Any(m => m.Id == movieId);
            return isValid;
        }
    }
}