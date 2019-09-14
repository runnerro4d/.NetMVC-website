namespace Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBchanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "cust_id", "dbo.Customers");
            DropIndex("dbo.Bookings", new[] { "cust_id" });
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.Staffs");
            AlterColumn("dbo.Bookings", "cust_id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Customers", "id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Staffs", "id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Customers", "id");
            AddPrimaryKey("dbo.Staffs", "id");
            CreateIndex("dbo.Bookings", "cust_id");
            AddForeignKey("dbo.Bookings", "cust_id", "dbo.Customers", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "cust_id", "dbo.Customers");
            DropIndex("dbo.Bookings", new[] { "cust_id" });
            DropPrimaryKey("dbo.Staffs");
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Staffs", "id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Customers", "id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Bookings", "cust_id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Staffs", "id");
            AddPrimaryKey("dbo.Customers", "id");
            CreateIndex("dbo.Bookings", "cust_id");
            AddForeignKey("dbo.Bookings", "cust_id", "dbo.Customers", "id", cascadeDelete: true);
        }
    }
}
