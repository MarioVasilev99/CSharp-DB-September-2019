namespace ProductShop
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using System.Linq;

    using ProductShop.Data;
    using ProductShop.Dtos.Import;
    using ProductShop.Dtos.Export;
    using ProductShop.Models;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Xml;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());

            using (var db = new ProductShopContext())
            {
                var xmlPath = "./../../../Datasets/categories-products.xml";
                var inputXml = File.ReadAllText(xmlPath);

                // Import Users - P01
                //Console.WriteLine(ImportUsers(db, inputXml));

                // Import Products - P02
                //Console.WriteLine(ImportProducts(db, inputXml));

                // Import Categories - P03
                //Console.WriteLine(ImportCategories(db, inputXml));

                // Import Categories-Products - P04
                //Console.WriteLine(ImportCategoryProducts(db, inputXml))

                // Get Products in Range - P05
                //Console.WriteLine(GetProductsInRange(db));

                // Get Sold Products - P06
                Console.WriteLine(GetSoldProducts(db));
            }
        }

        // Import Users - P01
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            // Initialize XML Serializer
            var xmlSerializer = new XmlSerializer(typeof(ImportUserDto[]),
                                new XmlRootAttribute("Users"));

            // Deserialize the XML to Dtos Array
            ImportUserDto[] userDtos;
            using (var reader = new StringReader(inputXml))
            {
                userDtos = (ImportUserDto[])xmlSerializer.Deserialize(reader);
            }

            // Map the dtos to Users Array And Import To DB
            var users = Mapper.Map<User[]>(userDtos);
            context.Users.AddRange(users);
            context.SaveChanges();

            // Return the count of imported users
            return $"Successfully imported {users.Length}";
        }

        // Import Products - P02
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            // Initialize XML Serializer
            var xmlSerializer = new XmlSerializer(typeof(ImportProductDto[]),
                                new XmlRootAttribute("Products"));

            // Deserialize the XML to Dtos Array
            ImportProductDto[] productDtos;
            using (var reader = new StringReader(inputXml))
            {
                productDtos = (ImportProductDto[])xmlSerializer.Deserialize(reader);
            }

            // Map the dtos to Products Array And Import To DB
            var products = Mapper.Map<Product[]>(productDtos);
            context.Products.AddRange(products);
            context.SaveChanges();

            // Return the count of imported products
            return $"Successfully imported {products.Length}";
        }

        // Import Categories - P03
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            // Initialize XML Serializer
            var xmlSerializer = new XmlSerializer(typeof(ImportCategoryDto[]),
                                new XmlRootAttribute("Categories"));

            // Deserialize the XML to Dtos Array
            ImportCategoryDto[] categoryDtos;
            using (var reader = new StringReader(inputXml))
            {
                categoryDtos = (ImportCategoryDto[])xmlSerializer.Deserialize(reader);
            }

            // Map the dtos to Categories Array And Import To DB
            var categories = Mapper.Map<Category[]>(categoryDtos);
            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Return the count of imported categories
            return $"Successfully imported {categories.Length}";
        }

        // Import Category-Products - P04
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            // Initialize XML Serializer
            var xmlSerializer = new XmlSerializer(typeof(ImportCategoryProductsDto[]),
                                new XmlRootAttribute("CategoryProducts"));

            // Deserialize the XML to Dtos Array
            ImportCategoryProductsDto[] categoryProductsDtos;
            using (var reader = new StringReader(inputXml))
            {
                categoryProductsDtos = (ImportCategoryProductsDto[])xmlSerializer.Deserialize(reader);
            }

            // Map the dtos to Categories-Products Array and Import to Db
            var categoriesProducts = Mapper.Map<CategoryProduct[]>(categoryProductsDtos);
            context.CategoryProducts.AddRange(categoriesProducts);
            context.SaveChanges();

            // Return the count of imported categories-products
            return $"Successfully imported {categoriesProducts.Length}";
        }

        // Export Products In Range - P05
        public static string GetProductsInRange(ProductShopContext context)
        {
            // Get Products From Db And Project them to DTO Array
            var products = context.Products
                                  .Where(p => p.Price >= 500 && p.Price <= 1000)
                                  .OrderBy(p => p.Price)
                                  .Take(10)
                                  .ProjectTo<ExportProductsInRangeDto>()
                                  .ToArray();

            // Initialize XML Serializer
            var xmlSerializer = new XmlSerializer(typeof(ExportProductsInRangeDto[]),
                                                  new XmlRootAttribute("Products"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            // Initialize StringBuilder And use StringWriter to write exported Xml
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                xmlSerializer.Serialize(writer, products, namespaces);
            }

            // Return the Serialized Products Array in XML Format
            return sb.ToString().TrimEnd();
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any())
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new ExportSoldProductsDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                        .Select(p => new ProductDto
                        {
                            Price = p.Price,
                            Name = p.Name
                        })
                        .ToArray()
                })
                .Take(5)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportSoldProductsDto[]),
                                                  new XmlRootAttribute("Users"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                xmlSerializer.Serialize(writer, users, namespaces);
            }

            var result = sb.ToString().TrimEnd();
            return result;
        }

    }
}