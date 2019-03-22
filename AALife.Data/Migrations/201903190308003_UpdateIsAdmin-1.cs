namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIsAdmin1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_UserTable", "IsAdmin", c => c.Boolean(nullable: false));
            DropColumn("dbo.tab_UserTable", "IsManager");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_UserTable", "IsManager", c => c.Boolean(nullable: false));
            DropColumn("dbo.tab_UserTable", "IsAdmin");
        }
    }
}
