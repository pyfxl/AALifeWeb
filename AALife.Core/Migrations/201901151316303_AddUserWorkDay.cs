namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserWorkDay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_UserTable", "UserWorkDay", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_UserTable", "UserWorkDay");
        }
    }
}
