namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_Permission", "ControllerName", c => c.String(maxLength: 20));
            DropColumn("dbo.tab_Permission", "ControllName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_Permission", "ControllName", c => c.String(maxLength: 20));
            DropColumn("dbo.tab_Permission", "ControllerName");
        }
    }
}
