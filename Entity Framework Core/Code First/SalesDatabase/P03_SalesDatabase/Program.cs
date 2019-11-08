namespace P03_SalesDatabase
{
    using System;
    using P03_SalesDatabase.Data;
    using Microsoft.EntityFrameworkCore;
    public class Program
    {
        public static void Main()
        {
            using (var db = new SalesContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
