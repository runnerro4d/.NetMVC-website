namespace Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Booking
    {
        public int id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int NumberOfPeople { get; set; }

        public double TotalCost { get; set; }

        [Required]
        [StringLength(128)]
        public string cust_id { get; set; }

        public int room_id { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Room Room { get; set; }
    }
}
