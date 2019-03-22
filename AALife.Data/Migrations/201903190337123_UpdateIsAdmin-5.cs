namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIsAdmin5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_UserTable", "IsAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_UserTable", "IsAdmin");
        }
    }
}
