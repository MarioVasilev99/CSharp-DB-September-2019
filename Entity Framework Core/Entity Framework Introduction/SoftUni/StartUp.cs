namespace SoftUni
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using SoftUni.Data;
    using SoftUni.Models;

    public class StartUp
    {
        public static void Main()
        {
            SoftUniContext context = new SoftUniContext();

            string result = GetEmployeesByFirstNameStartingWithSa(context);

            Console.WriteLine(result);
        }

        /* --Problem-03-- */
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

        /* --Problem-04-- */
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

        /* --Problem-05-- */
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

        /* --Problem-06-- */
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var newAddres = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var employee = context
                .Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            employee.Address = newAddres;
            context.SaveChanges();

            var employees = context
                .Employees
                .OrderByDescending(e => e.AddressId)
                .Select(e => new
                {
                    e.Address.AddressText
                })
                .Take(10)
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var emp in employees)
            {
                var lineToAppend = $"{emp.AddressText}";

                resultSb
                    .AppendLine(lineToAppend);
            }

            return resultSb.ToString().TrimEnd();
        }

        /* --Problem-07-- */
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context
                .Employees
                .Where(e => e.EmployeesProjects
                    .Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    Name = $"{e.FirstName} {e.LastName}",
                    ManagerName = $"{e.Manager.FirstName} {e.Manager.LastName}",
                    Projects = e.EmployeesProjects
                        .Where(p => p.EmployeeId == e.EmployeeId)
                        .Select(p => new
                        {
                            Name = p.Project.Name,
                            p.Project.StartDate,
                            p.Project.EndDate
                        })
                        .ToList(),
                })
                .Take(10)
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var emp in employees)
            {
                var employeeInfoLine = $"{emp.Name} - Manager: {emp.ManagerName}";

                resultSb.AppendLine(employeeInfoLine);

                foreach (var project in emp.Projects)
                {
                    var projectStardDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    var projectEndDate = "";

                    if (project.EndDate == null)
                    {
                        projectEndDate = "not finished";
                    }
                    else
                    {
                        projectEndDate = project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    }
                    
                    var projectInfoLine = $"--{project.Name} - {projectStardDate} - {projectEndDate}";
                    resultSb.AppendLine(projectInfoLine);
                }
            }


            return resultSb.ToString().TrimEnd();
        }

        /* --Problem-08-- */
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var adresses = context
                .Addresses
                .Select(a => new
                {
                    Text = a.AddressText,
                    TownName = a.Town.Name,
                    EmployeesCount = a.Employees.Count()
                })
                .OrderByDescending(a => a.EmployeesCount)
                .ThenBy(a => a.TownName)
                .ThenBy(a => a.Text)
                .Take(10)
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var address in adresses)
            {
                var addressInfoLine = $"{address.Text}, {address.TownName} - {address.EmployeesCount} employees";

                resultSb
                    .AppendLine(addressInfoLine);
            }

            return resultSb.ToString().TrimEnd();
        }

        /* --Problem-09-- */
        public static string GetEmployee147(SoftUniContext context)
        {
            var employee = context
                .Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e => new
                {
                    Name = $"{e.FirstName} {e.LastName}",
                    e.JobTitle,
                    Projects = e.EmployeesProjects
                        .Where(p => p.EmployeeId == e.EmployeeId)
                        .Select(p => new
                        {
                            Name = p.Project.Name
                        })
                        .OrderBy(p => p.Name)
                        .ToList()
                })
                .FirstOrDefault();

            var resultSb = new StringBuilder();

            var employeeInfoLine = $"{employee.Name} - {employee.JobTitle}";
            resultSb.AppendLine(employeeInfoLine);

            foreach (var project in employee.Projects)
            {
                var projectInfoLine = $"{project.Name}";
                resultSb.AppendLine(projectInfoLine);
            }

            return resultSb.ToString().TrimEnd();
        }

        /* --Problem-10-- */
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context
                .Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    d.Name,
                    ManagerName = $"{d.Manager.FirstName} {d.Manager.LastName}",
                    Employees = d.Employees
                        .OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName)
                        .Select(e => new
                        {
                            Name = $"{e.FirstName} {e.LastName}",
                            e.JobTitle
                        })
                        .ToList()
                })
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var department in departments)
            {
                var departmentInfo = $"{department.Name} - {department.ManagerName}";
                resultSb.AppendLine(departmentInfo);

                foreach (var employee in department.Employees)
                {
                    var employeeInfo = $"{employee.Name} - {employee.JobTitle}";
                    resultSb.AppendLine(employeeInfo);
                }
            }

            return resultSb.ToString().TrimEnd();
        }

        /* --Problem-11-- */
        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context
                .Projects
                .OrderByDescending(p => p.StartDate)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                })
                .Take(10)
                .OrderBy(p => p.Name)
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var project in projects)
            {
                resultSb.AppendLine(project.Name);
                resultSb.AppendLine(project.Description);
                resultSb.AppendLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt"));
            }

            return resultSb.ToString().TrimEnd();
        }

        /* --Problem-12-- */
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var departmentsNeeded = new string[4]
            {
                "Engineering",
                "Tool Design",
                "Marketing",
                "Information Services"
            };

            var employees = context
                .Employees
                .Where(e => departmentsNeeded.Contains(e.Department.Name))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            employees.ForEach(e => e.Salary += e.Salary * 0.12m);
            context.SaveChanges();
            
            var resultSb = new StringBuilder();

            foreach (var employee in employees)
            {
                resultSb
                    .AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
            }
            

            return resultSb.ToString().TrimEnd();
        }

        /* --Problem-12-- */
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context
                .Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => new
                {
                    Name = $"{e.FirstName} {e.LastName}",
                    e.JobTitle,
                    e.Salary
                })
                .ToList();

            var resultSb = new StringBuilder();

            foreach (var employee in employees)
            {
                var employeeInfoLine = $"{employee.Name} - {employee.JobTitle} - (${employee.Salary:f2})";

                resultSb.AppendLine(employeeInfoLine);
            }

            return resultSb.ToString().TrimEnd();
        }
    }
}
