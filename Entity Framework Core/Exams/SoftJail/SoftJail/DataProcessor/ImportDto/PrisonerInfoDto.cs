namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Prisoner")]
    public class PrisonerInfoDto
    {
        [XmlAttribute("id")]
        [Required]
        public int Id { get; set; }
    }
}
