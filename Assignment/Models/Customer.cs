using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class Customer
    {
        [Display(Name = "Customer ID")]
        public int id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public String FName { get; set; }

        [Display(Name = "Last Name")]
        public String LName { get; set; }

        [Display(Name = "Date Of Registration")]
        public DateTime DateOfRegistration { get; set; }

    }
}