namespace Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rating
    {
        public int id { get; set; }
        [DisplayName("Rating")]
        [Range(1, 5)]
        public int rate { get; set; }
        [DisplayName("Comments")]
        public string comments { get; set; }
        [DisplayName("Date of Review")]
        public DateTime ratingDate { get; set; }
        [DisplayName("Booking ID")]
        public int booking_id { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
