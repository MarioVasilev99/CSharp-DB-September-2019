namespace P01_StudentSystem.Data.Models
{
    using Enumerations;
    using System.ComponentModel.DataAnnotations;

    using static DataValidations.Resource;
    public class Resource
    {
        public int ResourceId { get; set; }

        [Required]
        [MaxLength(MaxNameLenght)]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public ResourceType ResourceType { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
