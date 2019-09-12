namespace Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        NumberOfPeople = c.Int(nullable: false),
                        TotalCost = c.Double(nullable: false),
                        cust_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customers", t => t.cust_id)
                .Index(t => t.cust_id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FName = c.String(),
                        LName = c.String(),
                        DateOfRegistration = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Floor = c.Int(nullable: false),
                        Description = c.String(),
                        PricePerNight = c.Double(nullable: false),
                        RoomCapacity = c.Int(nullable: false),
                        hotel_id = c.Int(),
                        Booking_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Hotels", t => t.hotel_id)
                .ForeignKey("dbo.Bookings", t => t.Booking_id)
                .Index(t => t.hotel_id)
                .Index(t => t.Booking_id);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Street = c.String(),
                        Suburb = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FName = c.String(),
                        LName = c.String(),
                        DateOfJoining = c.DateTime(nullable: false),
                        DateOfTermination = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "Booking_id", "dbo.Bookings");
            DropForeignKey("dbo.Rooms", "hotel_id", "dbo.Hotels");
            DropForeignKey("dbo.Bookings", "cust_id", "dbo.Customers");
            DropIndex("dbo.Rooms", new[] { "Booking_id" });
            DropIndex("dbo.Rooms", new[] { "hotel_id" });
            DropIndex("dbo.Bookings", new[] { "cust_id" });
            DropTable("dbo.Staffs");
            DropTable("dbo.Hotels");
            DropTable("dbo.Rooms");
            DropTable("dbo.Customers");
            DropTable("dbo.Bookings");
        }
    }
}
