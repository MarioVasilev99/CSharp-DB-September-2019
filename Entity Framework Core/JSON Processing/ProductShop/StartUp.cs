namespace ProductShop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    using ProductShop.Data;
    using ProductShop.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new ProductShopContext())
            {
                //var jsonPath = "./../../../Datasets/categories-products.json";
                //var json = File.ReadAllText(jsonPath);

                var result = GetUsersWithProducts(db);
                Console.WriteLine(result);
            }
        }

        // Problem 01
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        // Problem 02
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        // Problem 03
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert
                .DeserializeObject<Category[]>(inputJson)
                .Where(c => c.Name != null)
                .ToArray();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
        }

        // Problem 04
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);
            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Length}";
        }

        // Problem 05
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                })
                .ToList();

            var productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);
            return productsJson;
        }

        // Problem 06
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                        .Select(ps => new
                        {
                            name = ps.Name,
                            price = ps.Price,
                            buyerFirstName = ps.Buyer.FirstName,
                            buyerLastName = ps.Buyer.LastName
                        })
                })
                .ToArray();

            var usersJson = JsonConvert.SerializeObject(users, Formatting.Indented);
            return usersJson;
        }

        // Problem 07
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                .Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count,
                    averagePrice = $"{c.CategoryProducts.Average(cp => cp.Product.Price):F2}",
                    totalRevenue = $"{c.CategoryProducts.Sum(cp => cp.Product.Price):F2}"
                })
                .ToArray();

            return JsonConvert
                .SerializeObject(categories, Formatting.Indented);
        }

        // Problem 08
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                .OrderByDescending(u => u.ProductsSold.Count(ps => ps.Buyer != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.ProductsSold.Count(ps => ps.Buyer != null),
                        products = u.ProductsSold
                            .Where(p => p.Buyer != null)
                            .Select(p => new
                            {
                                name = p.Name,
                                price = p.Price
                            })
                            .ToArray()
                    }
                })
                .ToArray();

            var usersFinalObj = new
            {
                usersCount = users.Length,
                users
            };


            return JsonConvert.SerializeObject(usersFinalObj,
                                               Formatting.Indented,
                                               new JsonSerializerSettings
                                               {
                                                   NullValueHandling = NullValueHandling.Ignore
                                               });
        }
    }
}