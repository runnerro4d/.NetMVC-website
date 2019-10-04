using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Assignment.Context
{
    public class HotelStuff : DbContext
    {
        public DbSet<Hotel> hotels { get; set; }
        public DbSet<Staff> staffs { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<Rating> ratings { get; set; }
    }
}