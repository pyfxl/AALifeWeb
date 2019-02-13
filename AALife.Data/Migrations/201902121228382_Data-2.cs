namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Data2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tab_UserTable", "UserId", "dbo.tab_UserTable");
            DropIndex("dbo.tab_UserTable", new[] { "UserId" });
            DropColumn("dbo.tab_UserTable", "Live");
            DropColumn("dbo.tab_UserTable", "Rank");
            DropColumn("dbo.tab_UserTable", "Image");
            DropColumn("dbo.tab_UserTable", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_UserTable", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.tab_UserTable", "Image", c => c.String(maxLength: 200));
            AddColumn("dbo.tab_UserTable", "Rank", c => c.Byte());
            AddColumn("dbo.tab_UserTable", "Live", c => c.Byte(nullable: false));
            CreateIndex("dbo.tab_UserTable", "UserId");
            AddForeignKey("dbo.tab_UserTable", "UserId", "dbo.tab_UserTable", "Id");
        }
    }
}
