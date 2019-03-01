namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermision1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_Permission", "ParentId", c => c.Int(nullable: false));
            DropColumn("dbo.tab_Permission", "ReportTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_Permission", "ReportTo", c => c.Int());
            DropColumn("dbo.tab_Permission", "ParentId");
        }
    }
}
