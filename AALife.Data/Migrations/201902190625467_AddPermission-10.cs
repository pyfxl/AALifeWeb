namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ParentId", c => c.Int());
            DropColumn("dbo.Employees", "ReportTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "ReportTo", c => c.Int());
            DropColumn("dbo.Employees", "ParentId");
        }
    }
}
