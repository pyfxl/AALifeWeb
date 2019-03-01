namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermision10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tab_Permission", "ControllerName", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tab_Permission", "ControllerName", c => c.String(maxLength: 20));
        }
    }
}
