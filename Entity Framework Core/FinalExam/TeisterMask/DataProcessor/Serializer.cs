namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context.Projects
                .Where(p => p.Tasks.Any())
                .OrderByDescending(p => p.Tasks.Count)
                .ThenBy(p => p.Name)
                .Select(p => new ExportProjectDto()
                {
                    TasksCount = p.Tasks.Count(),
                    ProjectName = p.Name,
                    HasEndDate = HasEndDate(p.DueDate),
                    Tasks = p.Tasks
                        .OrderBy(t => t.Name)
                        .Select(t => new ExportTaskDto()
                        {
                            Name = t.Name,
                            Label = t.LabelType.ToString()
                        })
                        .ToArray()
                }).ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportProjectDto[]),
                                                  new XmlRootAttribute("Projects"));
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(writer, projects, namespaces);

            var result = sb.ToString().TrimEnd();
            return result;
        }

        private static string HasEndDate(DateTime? dueDate)
        {
            if (dueDate == null)
            {
                return "No";
            }

            return "Yes";
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            //IsTasksValid(e.EmployeesTasks, date)
            var employees = context.Employees
                .Where(e => e.EmployeesTasks.Any(t => t.Task.OpenDate >= date))
                .OrderByDescending(e => e.EmployeesTasks.Count(et => et.Task.OpenDate >= date))
                .ThenBy(e => e.Username)
                .Take(10)
                .Select(e => new
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                        .Where(t => t.Task.OpenDate >= date)
                        .OrderByDescending(et => et.Task.DueDate)
                        .ThenBy(t => t.Task.Name)
                        .Select(t => new
                        {
                            TaskName = t.Task.Name,
                            OpenDate = t.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                            DueDate = t.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = t.Task.LabelType.ToString(),
                            ExecutionType = t.Task.ExecutionType.ToString()
                        })
                        .ToArray()
                })
                .ToArray();

            var json = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return json.ToString();
        }

        private static bool AreTasksValid(ICollection<EmployeeTask> employeesTasks, DateTime date)
        {
            var areValid = true;

            foreach (var task in employeesTasks)
            {
                if (!(task.Task.OpenDate >= date))
                {
                    areValid = false;
                }
            }

            return areValid;
        }
    }
}