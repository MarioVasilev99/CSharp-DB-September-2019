namespace BookShop
{
    using BookShop.Models;
    using Data;

    using System;
    using System.Linq;
    using System.Text;
    using Z.EntityFramework.Plus;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                var userCommand = Console.ReadLine();

                IncreasePrices(db);
            }
        }

        //Problem 01
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var bookTitles = context
                .Books
                .Where(b => b.AgeRestriction.ToString().ToLower() == command.ToLower())
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var title in bookTitles)
            {
                resultSb.AppendLine(title);
            }

            return resultSb.ToString().TrimEnd();
        }

        //Problem 02
        public static string GetGoldenBooks(BookShopContext context)
        {
            var goldenBooks = context
                .Books
                .Where(b => b.EditionType.ToString() == "Gold")
                .Where(b => b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => new { b.Title })
                .ToList();

            var resultSb = new StringBuilder();

            goldenBooks
                .ForEach(b => resultSb.AppendLine(b.Title));

            return resultSb.ToString().TrimEnd();
        }

        //Problem 03
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context
                .Books
                .Where(b => b.Price > 40)
                .Select(b => new { b.Title, b.Price })
                .OrderByDescending(b => b.Price)
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var book in books)
            {
                var lineToAppend = $"{book.Title} - ${book.Price:F2}";

                resultSb.AppendLine(lineToAppend);
            }

            return resultSb.ToString().TrimEnd();
        }

        //Problem 04
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        //Problem 05
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.Split(" ").Select(c => c.ToLower()).ToList();

            var books = context
                .Books
                .Where(b => b.BookCategories
                             .Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        //Problem 06
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var maxReleaseDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            var books = context
                .Books
                .Where(b => b.ReleaseDate < maxReleaseDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.Price,
                    b.EditionType
                })
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var book in books)
            {
                var bookPresenationLine = $"{book.Title} - {book.EditionType} - ${book.Price:F2}";

                resultSb.AppendLine(bookPresenationLine);
            }

            return resultSb.ToString().TrimEnd();
        }

        //Problem 07
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context
                .Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = $"{a.FirstName} {a.LastName}"
                })
                .OrderBy(a => a.FullName)
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var author in authors)
            {
                resultSb.AppendLine(author.FullName);
            }

            return resultSb.ToString().TrimEnd();
        }

        //Problem 08
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var stringToContain = input.ToLower();

            var books = context
                .Books
                .Where(b => b.Title.ToLower().Contains(stringToContain))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        //Problem 09
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var stringToStartWith = input.ToLower();

            var books = context
                .Books
                .Where(b => b
                             .Author.LastName
                             .ToLower()
                             .StartsWith(stringToStartWith))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title,
                    AuthorName = $"{b.Author.FirstName} {b.Author.LastName}"
                })
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var book in books)
            {
                var bookAuthorInfoLine = $"{book.Title} ({book.AuthorName})";

                resultSb.AppendLine(bookAuthorInfoLine);
            }

            return resultSb.ToString().TrimEnd();
        }

        //Problem 10
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var booksCount = context
                .Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();

            return booksCount;
        }

        //Problem 11
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context
                .Authors
                .Select(a => new
                {
                    Name = $"{a.FirstName} {a.LastName}",
                    CopiesCount = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.CopiesCount)
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var author in authors)
            {
                var authorBookInfoLine = $"{author.Name} - {author.CopiesCount}";

                resultSb.AppendLine(authorBookInfoLine);
            }

            return resultSb.ToString().TrimEnd();
        }

        //Problem 12
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context
                .Categories
                .Select(c => new
                {
                    c.Name,
                    TotalProfit = c.CategoryBooks.Sum(b => b.Book.Price * b.Book.Copies)
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.Name)
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var category in categories)
            {
                var categoryProfitLine = $"{category.Name} ${category.TotalProfit:F2}";

                resultSb.AppendLine(categoryProfitLine);
            }

            return resultSb.ToString().TrimEnd();
        }

        //Problem 13
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context
                .Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    c.Name,
                    Books = c.CategoryBooks
                                .OrderByDescending(b => b.Book.ReleaseDate)
                                .Select(b => new
                                {
                                    Title = b.Book.Title,
                                    Year = b.Book.ReleaseDate.Value.Year
                                })
                                .Take(3)
                                .ToList()
                })
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var category in categories)
            {
                resultSb.AppendLine($"--{category.Name}");

                foreach (var book in category.Books)
                {
                    var bookInfoLine = $"{book.Title} ({book.Year})";
                    resultSb.AppendLine(bookInfoLine);
                }
            }

            return resultSb.ToString().TrimEnd();
        }

        //Problem 14
        public static void IncreasePrices(BookShopContext context)
        {
            context
                .Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .Update(b => new Book { Price = b.Price + 5 });
        }
    }
}
