namespace P01_HospitalDatabase 
{
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data;

    public class Program
    {
        public static void Main()
        {
            using (var db = new HospitalContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
