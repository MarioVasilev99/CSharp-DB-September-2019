namespace SoftJail.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var departments = new List<Department>();
            var importDepartmentDtos = JsonConvert.DeserializeObject<ImportDepartmentDto[]>(jsonString);

            foreach (var dto in importDepartmentDtos)
            {
                var isValid = IsValid(dto) &&
                    dto.Cells.All(IsValid);

                if (isValid)
                {
                    var cells = new List<Cell>();

                    foreach (var cell in dto.Cells)
                    {
                        var newCell = new Cell()
                        {
                            CellNumber = cell.CellNumber,
                            HasWindow = cell.HasWindow
                        };

                        cells.Add(newCell);
                    }

                    var newDepartment = new Department()
                    {
                        Name = dto.Name,
                        Cells = cells
                    };

                    departments.Add(newDepartment);
                    sb.AppendLine($"Imported {newDepartment.Name} with {cells.Count} cells");
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var prisonersEmailsDtos = JsonConvert.DeserializeObject<ImportPrisonerDto[]>(jsonString);

            var resultSb = new StringBuilder();
            var prisoners = new List<Prisoner>();

            foreach (var prisonerDto in prisonersEmailsDtos)
            {
                var isValid = IsValid(prisonerDto) &&
                    prisonerDto.Mails.All(IsValid);

                if (isValid)
                {
                    var prisonerMails = new List<Mail>();

                    foreach (var mail in prisonerDto.Mails)
                    {
                        var newMail = new Mail()
                        {
                            Description = mail.Description,
                            Sender = mail.Sender,
                            Address = mail.Address
                        };

                        prisonerMails.Add(newMail);
                    }

                    var prisoner = new Prisoner()
                    {
                        FullName = prisonerDto.FullName,
                        Nickname = prisonerDto.Nickname,
                        Age = prisonerDto.Age,
                        IncarcerationDate = DateTime.ParseExact(
                            prisonerDto.IncarcerationDate,
                            "dd/MM/yyyy",
                            CultureInfo.InvariantCulture),
                        ReleaseDate = prisonerDto.ReleaseDate == null
                            ? new DateTime?()
                            : DateTime.ParseExact(
                                prisonerDto.ReleaseDate,
                                "dd/MM/yyyy",
                                CultureInfo.InvariantCulture),
                        Bail = prisonerDto.Bail,
                        CellId = prisonerDto.CellId,
                        Mails = prisonerMails
                    };

                    prisoners.Add(prisoner);
                    resultSb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
                }
                else
                {
                    resultSb.AppendLine(ErrorMessage);
                }
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            return resultSb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportOfficerDto[]),
                                                  new XmlRootAttribute("Officers"));

            var officersPrisonersDto = (ImportOfficerDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var resultSb = new StringBuilder();
            var officers = new List<Officer>();

            foreach (var officerDto in officersPrisonersDto)
            {
                var isPositionValid = Enum.TryParse(officerDto.Position, out Position position);
                var isWeaponValid = Enum.TryParse(officerDto.Weapon, out Weapon weapon);

                var isValid = IsValid(officerDto) &&
                    isPositionValid &&
                    isWeaponValid;

                if (isValid)
                {
                    var prisoners = new List<OfficerPrisoner>();
                    foreach (var prisonerDto in officerDto.Prisoners)
                    {
                        var prisoner = new OfficerPrisoner()
                        {
                            PrisonerId = prisonerDto.Id
                        };

                        prisoners.Add(prisoner);
                    }

                    var officer = new Officer()
                    {
                        FullName = officerDto.FullName,
                        Salary = officerDto.Salary,
                        Position = position,
                        Weapon = weapon,
                        DepartmentId = officerDto.DepartmentId,
                        OfficerPrisoners = prisoners
                    };

                    officers.Add(officer);
                    resultSb.AppendLine($"Imported {officer.FullName} ({prisoners.Count} prisoners)");
                }
                else
                {
                    resultSb.AppendLine(ErrorMessage);
                }
            }

            context.Officers.AddRange(officers);
            context.SaveChanges();

            return resultSb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}