using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

using CarDealer.Data;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new CarDealerContext())
            {
                // Problem 09
                //var jsonPath = $"./../../../Datasets/suppliers.json";
                //var json = File.ReadAllText(jsonPath);
                //Console.WriteLine(ImportSuppliers(db, json));

                // Problem 10
                //var jsonPath = $"./../../../Datasets/parts.json";
                //var json = File.ReadAllText(jsonPath);
                //Console.WriteLine(ImportParts(db, json));

                // Problem 11
                //var jsonPath = $"./../../../Datasets/cars.json";
                //var json = File.ReadAllText(jsonPath);
                //Console.WriteLine(ImportCars(db, json));

                // Problem 12
                //var jsonPath = $"./../../../Datasets/customers.json";
                //var json = File.ReadAllText(jsonPath);
                //Console.WriteLine(ImportCustomers(db, json));

                //Problem 13
                //var jsonPath = $"./../../../Datasets/sales.json";
                //var json = File.ReadAllText(jsonPath);
                //Console.WriteLine(ImportSales(db, json));

                //// Problem 14
                //Console.WriteLine(GetOrderedCustomers(db));

                // Problem 15
                //Console.WriteLine(GetCarsFromMakeToyota(db));

                //Problem 16
                Console.WriteLine(GetCarsWithTheirListOfParts(db));
            }
        }

        // Problem 09
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}.";
        }

        // Problem 10
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var partsUnfiltered = JsonConvert.DeserializeObject<Part[]>(inputJson);
            var existingSuppliersId = context.Suppliers.Select(s => s.Id).ToList();

            var parts = partsUnfiltered.Where(p => existingSuppliersId.Contains(p.SupplierId)).ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}.";
        }

        // Problem 11
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var cars = JsonConvert.DeserializeObject<Car[]>(inputJson);

            context.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Length}.";
        }

        // Problem 12
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}.";
        }

        // Problem 13
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            context.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Length}.";
        }

        // Problem 14
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    c.IsYoungDriver
                })
                .ToList();

            return JsonConvert.SerializeObject(customers, Formatting.Indented);
        }

        // Problem 15
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var toyotaCars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();

            return JsonConvert.SerializeObject(toyotaCars, Formatting.Indented);
        }

        // Problem 16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count()
                })
                .ToList();

            return JsonConvert.SerializeObject(suppliers, Formatting.Indented);
        }

        // Problem 17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    },
                    parts = c.PartCars
                             .Select(pc => new
                             {
                                 Name = pc.Part.Name,
                                 Price = $"{pc.Part.Price:F2}"
                             })
                             .ToArray()
                })
                .ToArray();

            return JsonConvert.SerializeObject(cars, Formatting.Indented);
        }
    }
}