namespace BookShop
{
    using Data;

    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                var userCommand = Console.ReadLine();

                var result = GetBookTitlesContaining(db, userCommand);
                Console.WriteLine(result);
            }
        }

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
    }
}
