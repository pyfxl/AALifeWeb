namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.usr_UserTable", "UserImage", c => c.String(maxLength: 200));
            AddColumn("dbo.usr_UserTable", "UserTheme", c => c.String(maxLength: 10));
            AddColumn("dbo.usr_UserTable", "UserLevel", c => c.Byte(nullable: false));
            AddColumn("dbo.usr_UserTable", "UserFrom", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.usr_UserTable", "Synchronize", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.usr_UserTable", "Synchronize");
            DropColumn("dbo.usr_UserTable", "UserFrom");
            DropColumn("dbo.usr_UserTable", "UserLevel");
            DropColumn("dbo.usr_UserTable", "UserTheme");
            DropColumn("dbo.usr_UserTable", "UserImage");
        }
    }
}
