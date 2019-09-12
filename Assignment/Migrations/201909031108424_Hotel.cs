namespace Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Hotel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "cust_id", "dbo.Customers");
            DropForeignKey("dbo.Rooms", "hotel_id", "dbo.Hotels");
            DropIndex("dbo.Bookings", new[] { "cust_id" });
            DropIndex("dbo.Rooms", new[] { "hotel_id" });
            AlterColumn("dbo.Bookings", "cust_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "FName", c => c.String(nullable: false));
            AlterColumn("dbo.Rooms", "hotel_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Hotels", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Hotels", "Street", c => c.String(nullable: false));
            AlterColumn("dbo.Hotels", "Suburb", c => c.String(nullable: false));
            CreateIndex("dbo.Bookings", "cust_id");
            CreateIndex("dbo.Rooms", "hotel_id");
            AddForeignKey("dbo.Bookings", "cust_id", "dbo.Customers", "id", cascadeDelete: true);
            AddForeignKey("dbo.Rooms", "hotel_id", "dbo.Hotels", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "hotel_id", "dbo.Hotels");
            DropForeignKey("dbo.Bookings", "cust_id", "dbo.Customers");
            DropIndex("dbo.Rooms", new[] { "hotel_id" });
            DropIndex("dbo.Bookings", new[] { "cust_id" });
            AlterColumn("dbo.Hotels", "Suburb", c => c.String());
            AlterColumn("dbo.Hotels", "Street", c => c.String());
            AlterColumn("dbo.Hotels", "Name", c => c.String());
            AlterColumn("dbo.Rooms", "hotel_id", c => c.Int());
            AlterColumn("dbo.Customers", "FName", c => c.String());
            AlterColumn("dbo.Bookings", "cust_id", c => c.Int());
            CreateIndex("dbo.Rooms", "hotel_id");
            CreateIndex("dbo.Bookings", "cust_id");
            AddForeignKey("dbo.Rooms", "hotel_id", "dbo.Hotels", "id");
            AddForeignKey("dbo.Bookings", "cust_id", "dbo.Customers", "id");
        }
    }
}
