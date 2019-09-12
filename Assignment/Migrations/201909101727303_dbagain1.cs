namespace Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbagain1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rooms", "Booking_id", "dbo.Bookings");
            DropIndex("dbo.Rooms", new[] { "Booking_id" });
            AddColumn("dbo.Bookings", "room_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Bookings", "room_id");
            AddForeignKey("dbo.Bookings", "room_id", "dbo.Rooms", "id", cascadeDelete: true);
            DropColumn("dbo.Rooms", "Booking_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "Booking_id", c => c.Int());
            DropForeignKey("dbo.Bookings", "room_id", "dbo.Rooms");
            DropIndex("dbo.Bookings", new[] { "room_id" });
            DropColumn("dbo.Bookings", "room_id");
            CreateIndex("dbo.Rooms", "Booking_id");
            AddForeignKey("dbo.Rooms", "Booking_id", "dbo.Bookings", "id");
        }
    }
}
