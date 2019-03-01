namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_Permission", "ParentId", c => c.Int(nullable: false));
            AddColumn("dbo.tab_Permission", "AreaName", c => c.String(maxLength: 20));
            AddColumn("dbo.tab_Permission", "ControllName", c => c.String(maxLength: 20));
            AddColumn("dbo.tab_Permission", "ActionName", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_Permission", "ActionName");
            DropColumn("dbo.tab_Permission", "ControllName");
            DropColumn("dbo.tab_Permission", "AreaName");
            DropColumn("dbo.tab_Permission", "ParentId");
        }
    }
}
