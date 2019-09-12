using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class Booking
    {
        [Display(Name = "Booking ID")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Room")]
        public Room room { get; set;}

        [Required]
        [Display(Name ="Customer Name")]
        public Customer cust { get; set; }

        [Required]
        [Display(Name = "Check-in Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Check-out Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Number of People")]
        public int NumberOfPeople { get; set; }

        [Display(Name = "Total Cost")]
        public double TotalCost { get; set; }
    }
}