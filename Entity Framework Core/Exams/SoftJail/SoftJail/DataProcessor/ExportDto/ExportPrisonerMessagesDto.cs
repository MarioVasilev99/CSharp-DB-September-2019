namespace SoftJail.DataProcessor.ExportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Message")]
    public class ExportPrisonerMessagesDto
    {
        [Required]
        [XmlElement("Description")]
        public string Description { get; set; }
    }
}
