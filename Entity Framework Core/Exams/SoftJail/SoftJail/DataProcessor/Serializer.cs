namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context.Prisoners
                .Where(p => ids.Contains(p.Id))
                .OrderBy(p => p.FullName)
                .ThenBy(p => p.Id)
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers
                        .Select(po => new
                        {
                            OfficerName = po.Officer.FullName,
                            Department = po.Officer.Department.Name
                        })
                        .OrderBy(o => o.OfficerName)
                        .ToArray(),
                    TotalOfficerSalary = p.PrisonerOfficers.Sum(o => o.Officer.Salary)
                })
                .ToArray();

            var json = JsonConvert.SerializeObject(prisoners, Newtonsoft.Json.Formatting.Indented);

            return json;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var prisonersNamesArray = prisonersNames.Split(",");

            var prisoners = context.Prisoners
                .Where(p => prisonersNamesArray.Contains(p.FullName))
                .OrderBy(p => p.FullName)
                .ThenBy(p => p.Id)
                .Select(p => new ExportPrisonerDto()
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd",
                                                                     CultureInfo.InvariantCulture),
                    EncryptedMessages = p.Mails
                        .Select(m => new ExportPrisonerMessagesDto()
                        {
                            Description = EncryptDescription(m.Description)
                        })
                        .ToArray()
                })
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportPrisonerDto[]), new XmlRootAttribute("Prisoners"));
            var resultSb = new StringBuilder();
            var writer = new StringWriter(resultSb);
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            xmlSerializer.Serialize(writer, prisoners, namespaces);

            return resultSb.ToString().TrimEnd();
        }

        private static string EncryptDescription(string description)
        {
            var descriptionCharArray = description.ToCharArray();
            var encryptedDescriptionArray = descriptionCharArray.Reverse();
            var encryptedDescription = string.Concat(encryptedDescriptionArray);
            return encryptedDescription;
        }
    }
}