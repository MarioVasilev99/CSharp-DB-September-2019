﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Task")]
    public class ExportTaskDto
    {
        //<Task>
        //  <Name>Broadleaf</Name>
        //  <Label>JavaAdvanced</Label>
        //</Task>

        [XmlElement("Name")]
        [Required]
        [MinLength(2), MaxLength(40)]
        public string Name { get; set; }

        [XmlElement("Label")]
        [Required]
        public string Label { get; set; }

    }
}
