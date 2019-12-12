﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Prisoner")]
    public class ExportPrisonerDto
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        [Required]
        [MinLength(3), MaxLength(20)]
        public string FullName { get; set; }

        [Required]
        [XmlElement("IncarcerationDate")]
        public string IncarcerationDate { get; set; }

        [XmlArray("EncryptedMessages")]
        public ExportPrisonerMessagesDto[] EncryptedMessages { get; set; }
    }
}
