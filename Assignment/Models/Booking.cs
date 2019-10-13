namespace Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Booking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            Ratings = new HashSet<Rating>();
        }

        public int id { get; set; }

        [DisplayName("Check-in Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("Check-out Date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Number of People")]
        [Range(1,50)]
        public int NumberOfPeople { get; set; }

        [DisplayName("Total Cost")]
        public double TotalCost { get; set; }

        [Required]
        [StringLength(128)]
        public string cust_id { get; set; }

        public int room_id { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Room Room { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
