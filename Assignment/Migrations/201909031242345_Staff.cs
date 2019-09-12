namespace Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Staff : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Staffs", "FName", c => c.String(nullable: false));
            AlterColumn("dbo.Staffs", "LName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staffs", "LName", c => c.String());
            AlterColumn("dbo.Staffs", "FName", c => c.String());
        }
    }
}
