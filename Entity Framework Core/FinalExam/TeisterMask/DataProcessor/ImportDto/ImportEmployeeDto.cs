namespace TeisterMask.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    public class ImportEmployeeDto
    {
        [MinLength(3), MaxLength(40)]
        [RegularExpression(@"^[A-Za-z0-9]+")]
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression("[0-9]{3}-[0-9]{3}-[0-9]{4}")]
        [Required]
        public string Phone { get; set; }

        public int[] Tasks { get; set; }
    }
}
