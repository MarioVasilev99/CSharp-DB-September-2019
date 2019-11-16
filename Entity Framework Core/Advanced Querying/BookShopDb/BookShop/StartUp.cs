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

                var result = GetGoldenBooks(db);
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
    }
}
