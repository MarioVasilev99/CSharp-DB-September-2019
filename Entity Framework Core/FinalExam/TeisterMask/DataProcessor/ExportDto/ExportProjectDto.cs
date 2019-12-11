using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ExportProjectDto
    {
        //    <Project TasksCount = "10" >
        //< ProjectName > Hyster - Yale </ ProjectName >
        //< HasEndDate > No </ HasEndDate >
        //< Tasks >

        [XmlAttribute("TasksCount")]
        public int TasksCount { get; set; }

        [XmlElement("ProjectName")]
        [Required]
        [MinLength(2), MaxLength(40)]
        public string ProjectName { get; set; }

        [XmlElement("HasEndDate")]
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public ExportTaskDto[] Tasks { get; set; }

    }
}
