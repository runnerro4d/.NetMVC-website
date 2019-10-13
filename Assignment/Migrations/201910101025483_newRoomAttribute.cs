namespace Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newRoomAttribute : DbMigration
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
                        cust_id = c.String(nullable: false, maxLength: 128),
                        room_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customers", t => t.cust_id, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.room_id, cascadeDelete: true)
                .Index(t => t.cust_id)
                .Index(t => t.room_id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        FName = c.String(nullable: false),
                        LName = c.String(),
                        DateOfRegistration = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        rate = c.Int(nullable: false),
                        comments = c.String(),
                        ratingDate = c.DateTime(nullable: false),
                        booking_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Bookings", t => t.booking_id, cascadeDelete: true)
                .Index(t => t.booking_id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Floor = c.Int(nullable: false),
                        RoomType = c.String(),
                        Description = c.String(),
                        PricePerNight = c.Double(nullable: false),
                        RoomCapacity = c.Int(nullable: false),
                        hotel_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Hotels", t => t.hotel_id, cascadeDelete: true)
                .Index(t => t.hotel_id);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        Suburb = c.String(nullable: false),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        FName = c.String(nullable: false),
                        LName = c.String(),
                        DateOfJoining = c.DateTime(nullable: false),
                        DateOfTermination = c.DateTime(nullable: false),
                        hotel_id = c.String(maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "hotel_id", "dbo.Hotels");
            DropForeignKey("dbo.Bookings", "room_id", "dbo.Rooms");
            DropForeignKey("dbo.Ratings", "booking_id", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "cust_id", "dbo.Customers");
            DropIndex("dbo.Rooms", new[] { "hotel_id" });
            DropIndex("dbo.Ratings", new[] { "booking_id" });
            DropIndex("dbo.Bookings", new[] { "room_id" });
            DropIndex("dbo.Bookings", new[] { "cust_id" });
            DropTable("dbo.Staffs");
            DropTable("dbo.Hotels");
            DropTable("dbo.Rooms");
            DropTable("dbo.Ratings");
            DropTable("dbo.Customers");
            DropTable("dbo.Bookings");
        }
    }
}
