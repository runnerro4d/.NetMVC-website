using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class Staff
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public String FName { get; set; }

        [Display(Name = "Last Name")]
        public String LName { get; set; }

        [Required]
        [Display(Name = "Date of Joining")]
        public DateTime DateOfJoining { get; set; }

        [Display(Name = "Date of Termination")]
        public DateTime DateOfTermination { get; set; }
    }
}