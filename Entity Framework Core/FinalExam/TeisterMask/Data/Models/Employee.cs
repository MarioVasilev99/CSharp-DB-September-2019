﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.Data.Models
{
    public class Employee
    {
        //•	Id - integer, Primary Key
        //•	Username - text with length[3, 40]. Should contain only lower or upper case letters and/or digits. (required)
        //•	Email – text(required). Validate it! There is attribute for this job.
        //•	Phone - text.Consists only of three groups(separated by '-'), the first two consist of three digits and the last one - of 4 digits. (required)
        //•	EmployeesTasks - collection of type EmployeeTask
        public Employee()
        {
            this.EmployeesTasks = new HashSet<EmployeeTask>();
        }

        [Key]
        public int Id { get; set; }

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

        //TODO
        public ICollection<EmployeeTask> EmployeesTasks { get; set; }
    }
}
