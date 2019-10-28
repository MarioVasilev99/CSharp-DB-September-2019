namespace AdoNetTest
{
    using System;
    using System.Data.SqlClient;
    public class Program
    {
        public static void Main()
        {
            var connectionString = @"Server=DESKTOP-P3QQLJA\SQLEXPRESS;Database=Softuni;Integrated Security=True";
            var connection = new SqlConnection(connectionString);

            connection.Open();

            using (connection)
            {
                var command = new SqlCommand(
                    "SELECT COUNT(*) FROM EMPLOYEES"
                    , connection);

                int result = (int)(command.ExecuteScalar());
                Console.WriteLine(result);
            }
        }
    }
}
