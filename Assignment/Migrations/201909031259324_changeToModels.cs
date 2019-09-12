namespace Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeToModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Staffs", "LName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staffs", "LName", c => c.String(nullable: false));
        }
    }
}
