using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class Hotel
    {
        [Display(Name = "Hotel ID")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Hotel Name")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Street")]
        public String Street { get; set; }

        [Required]
        [Display(Name = "Suburb")]
        public String Suburb { get; set; }

        [Display(Name = "State")]
        public String State { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }
    }
}