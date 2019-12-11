namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ImportDto;
    using System.Text;
    using TeisterMask.Data.Models;
    using System.Linq;
    using System.Xml.Serialization;
    using System.IO;
    using System.Globalization;
    using TeisterMask.Data.Models.Enums;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportProjectDto[]),
                                                  new XmlRootAttribute("Projects"));

            var reader = new StringReader(xmlString);
            var customerDtos = (ImportProjectDto[])xmlSerializer.Deserialize(reader);
            var resultSb = new StringBuilder();
            var validProjects = new List<Project>();

            foreach (var projectDto in customerDtos)
            {
                var isValid = IsValid(projectDto);

                if (isValid)
                {
                    var validTasks = new List<Task>();

                    var projectOpenDate = DateTime.ParseExact(
                        projectDto.OpenDate,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);

                    var projectDueDate = string.IsNullOrEmpty(projectDto.DueDate)
                        ? new DateTime?()
                        : DateTime.ParseExact(
                        projectDto.DueDate,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);

                    foreach (var dto in projectDto.Tasks)
                    {
                        var taskOpenDate = DateTime.ParseExact(
                        dto.OpenDate,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);

                        var taskDueDate = DateTime.ParseExact(
                        dto.DueDate,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);

                        var isExecutionTypeValid = Enum.TryParse(dto.ExecutionType, out ExecutionType execType);
                        var isLabelTypeValid = Enum.TryParse(dto.LabelType, out LabelType labelType);

                        var taskIsValid = IsValid(dto);
                        var isOpenDateValid = taskOpenDate > projectOpenDate;
                        var isDueDateValid = string.IsNullOrEmpty(projectDto.DueDate)
                            ? true
                            : taskDueDate < projectDueDate;


                        var isTaskValid = taskIsValid
                            && isExecutionTypeValid &&
                            isLabelTypeValid &&
                            isOpenDateValid &&
                            isDueDateValid;

                        if (isTaskValid)
                        {
                            var newTask = new Task()
                            {
                                Name = dto.Name,
                                OpenDate = taskOpenDate,
                                DueDate = taskDueDate,
                                ExecutionType = execType,
                                LabelType = labelType
                            };

                            validTasks.Add(newTask);
                        }
                        else
                        {
                            resultSb.AppendLine(ErrorMessage);
                            continue;
                        }
                    }

                    var project = new Project()
                    {
                        Name = projectDto.Name,
                        OpenDate = projectOpenDate,
                        DueDate = projectDueDate,
                        Tasks = validTasks
                    };

                    validProjects.Add(project);
                    resultSb.AppendLine(string.Format(SuccessfullyImportedProject,
                                    project.Name,
                                    project.Tasks.Count));
                }
                else
                {
                    resultSb.AppendLine(ErrorMessage);
                    continue;
                }
            }

            context.Projects.AddRange(validProjects);
            context.SaveChanges();

            return resultSb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employeeDtos = JsonConvert.DeserializeObject<ImportEmployeeDto[]>(jsonString);

            var resultSb = new StringBuilder();
            var validEmployees = new List<Employee>();

            foreach (var empDto in employeeDtos)
            {
                if (!IsValid(empDto))
                {
                    resultSb.AppendLine(ErrorMessage);
                    continue;
                }

                var employee = new Employee()
                {
                    Username = empDto.Username,
                    Phone = empDto.Phone,
                    Email = empDto.Email,
                };

                context.Employees.Add(employee);

                var validTasks = new List<EmployeeTask>();
                var uniqueTasks = empDto.Tasks.Distinct().ToArray();

                for (int i = 0; i < uniqueTasks.Length; i++)
                {
                    if (!context.Tasks.Any(t => t.Id == uniqueTasks[i]))
                    {
                        resultSb.AppendLine(ErrorMessage);
                        continue;
                    }
                    var employeeTask = new EmployeeTask()
                    {
                        EmployeeId = employee.Id,
                        TaskId = uniqueTasks[i]
                    };

                    context.EmployeesTasks.Add(employeeTask);
                }

                context.SaveChanges();

                resultSb.AppendLine(string.Format(SuccessfullyImportedEmployee,
                                    employee.Username, employee.EmployeesTasks.Count));
            }


            var result = resultSb.ToString().TrimEnd();
            return result;
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}