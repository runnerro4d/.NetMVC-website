namespace Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff
    {
        public string id { get; set; }

        [Required]
        public string FName { get; set; }

        public string LName { get; set; }

        public DateTime DateOfJoining { get; set; }

        public DateTime DateOfTermination { get; set; }

        [StringLength(10)]
        public string hotel_id { get; set; }
    }
}
