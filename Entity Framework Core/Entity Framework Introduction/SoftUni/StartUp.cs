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
            SoftUniContext context = new SoftUniContext();

            string result = GetEmployeesFromResearchAndDevelopment(context);

            Console.WriteLine(result);
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var empInfo = context.Employees
                           .Select(e => new
                           {
                               Id = e.EmployeeId,
                               Name = String.Join(" ", e.FirstName, e.LastName, e.MiddleName),
                               e.JobTitle,
                               e.Salary
                           })
                           .OrderBy(e => e.Id);

            var empInfoSb = new StringBuilder();

            foreach (var emp in empInfo)
            {
                empInfoSb
                    .AppendLine($"{emp.Name} {emp.JobTitle} {emp.Salary:F2}");
            }

            return empInfoSb.ToString().TrimEnd();
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

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context
                .Employees
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => new
                {
                    Name = $"{e.FirstName} {e.LastName}",
                    Salary = e.Salary
                })
                .ToList();

            var employeesInfoSb = new StringBuilder();

            foreach (var employee in employees)
            {
                var employeeInfoLine =
                    $"{employee.Name} from Research and Development - ${employee.Salary:F2}";

                employeesInfoSb
                    .AppendLine(employeeInfoLine);
            }

            return employeesInfoSb.ToString().TrimEnd();
        }
    }
}
