namespace P01_StudentSystem
{
    using P01_StudentSystem.Data;
    using Microsoft.EntityFrameworkCore;
    public class Program
    {
        public static void Main()
        {
            using (var db = new StudentSystemContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
