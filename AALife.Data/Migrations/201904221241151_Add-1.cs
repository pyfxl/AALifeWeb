namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.usr_UserTablePositions", "IsMainPosition", c => c.Boolean());
            AddColumn("dbo.usr_UserTablePositions", "IsDeptmentLeader", c => c.Boolean());
            DropColumn("dbo.usr_UserTablePositions", "MainPosition");
        }
        
        public override void Down()
        {
            AddColumn("dbo.usr_UserTablePositions", "MainPosition", c => c.Boolean());
            DropColumn("dbo.usr_UserTablePositions", "IsDeptmentLeader");
            DropColumn("dbo.usr_UserTablePositions", "IsMainPosition");
        }
    }
}
