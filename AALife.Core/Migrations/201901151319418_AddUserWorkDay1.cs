namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserWorkDay1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tab_UserTable", "UserWorkDay", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tab_UserTable", "UserWorkDay", c => c.Int());
        }
    }
}
