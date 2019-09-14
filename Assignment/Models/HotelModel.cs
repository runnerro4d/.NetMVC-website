namespace Assignment.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HotelModel : DbContext
    {
        public HotelModel()
            : base("name=HotelModel")
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.cust_id);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.Hotel)
                .HasForeignKey(e => e.hotel_id);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Room)
                .HasForeignKey(e => e.room_id);
        }
    }
}
