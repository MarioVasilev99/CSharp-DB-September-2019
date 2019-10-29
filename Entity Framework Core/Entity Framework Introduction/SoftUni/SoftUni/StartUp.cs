namespace SoftUni
{
    using System;
    using System.Linq;
    using System.Text;
    using SoftUni.Data;

    public class StartUp
    {
        public static void Main()
        {
            var context = new SoftUniContext();

            var result = GetEmployeesWithSalaryOver50000(context);
            Console.WriteLine(result); 
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var empInfo = context.Employees
                           .OrderBy(e => e.EmployeeId)
                           .Select(e => new
                           {
                               e.FirstName,
                               e.MiddleName,
                               e.LastName,
                               e.JobTitle,
                               e.Salary
                           })
                           .ToList();

            var empInfoSb = new StringBuilder();

            foreach (var emp in empInfo)
            {
                empInfoSb.AppendLine($"{emp.FirstName} {emp.LastName} {emp.MiddleName} {emp.JobTitle} {emp.Salary:F2}");
            }

            return empInfoSb.ToString();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                                   .Where(e => e.Salary > 50000)
                                   .Select(e => new
                                   {
                                       e.FirstName,
                                       e.Salary
                                   })
                                   .OrderBy(e => e.FirstName)
                                   .ToList();

            var resultSb = new StringBuilder();

            foreach (var emp in employees)
            {
                var lineToAppend = $"{emp.FirstName} - {emp.Salary:F2}";
                resultSb.AppendLine(lineToAppend);
            }

            return resultSb.ToString().TrimEnd();
        }
    }
}
