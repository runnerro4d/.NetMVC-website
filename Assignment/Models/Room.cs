namespace Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            Bookings = new HashSet<Booking>();
        }

        public int id { get; set; }

        public int Floor { get; set; }

        public string Description { get; set; }

        [DisplayName("Price Per Night")]
        [Range(1,1000)]
        public double PricePerNight { get; set; }

        [DisplayName("Max Occupancy")]
        [Range(1, 10)]
        public int RoomCapacity { get; set; }

        public int hotel_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
