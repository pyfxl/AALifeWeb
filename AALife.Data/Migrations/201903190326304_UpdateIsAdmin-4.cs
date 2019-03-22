namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIsAdmin4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tab_UserTable", "IsAdmin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_UserTable", "IsAdmin", c => c.Boolean(nullable: false));
        }
    }
}
