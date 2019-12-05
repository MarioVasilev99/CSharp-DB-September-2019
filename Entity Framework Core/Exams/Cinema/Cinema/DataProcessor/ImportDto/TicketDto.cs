
namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Ticket")]
    public class TicketDto
    {
        [XmlElement("Price")]
        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335M")]
        public decimal Price { get; set; }

        [XmlElement("ProjectionId")]
        [Required]
        public int ProjectionId { get; set; }
    }
}
