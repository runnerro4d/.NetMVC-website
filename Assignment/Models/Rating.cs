namespace Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rating
    {
        public int id { get; set; }

        public int rate { get; set; }

        public string comments { get; set; }

        public DateTime ratingDate { get; set; }

        public int booking_id { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
