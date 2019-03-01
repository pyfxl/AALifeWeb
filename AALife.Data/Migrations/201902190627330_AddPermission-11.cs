namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ReportTo", c => c.Int());
            DropColumn("dbo.Employees", "ParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "ParentId", c => c.Int());
            DropColumn("dbo.Employees", "ReportTo");
        }
    }
}
