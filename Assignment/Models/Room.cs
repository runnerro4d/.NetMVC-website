using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class Room
    {
        [Display(Name = "Room Number")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Floor")]
        public int Floor { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }

        [Display(Name = "Price")]
        public double PricePerNight { get; set; }

        [Required]
        [Display(Name = "Capacity")]
        public int RoomCapacity { get; set; }

        [Required]
        [Display(Name = "Hotel")]
        public Hotel hotel { get; set; }
    }
}