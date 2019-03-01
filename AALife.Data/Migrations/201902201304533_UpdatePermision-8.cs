namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermision8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tab_OAuthTable", "UserId", "dbo.tab_UserTable");
            DropIndex("dbo.tab_OAuthTable", new[] { "UserId" });
            AddColumn("dbo.tab_OAuthTable", "UserTable_Id", c => c.Int());
            CreateIndex("dbo.tab_OAuthTable", "UserTable_Id");
            AddForeignKey("dbo.tab_OAuthTable", "UserTable_Id", "dbo.tab_UserTable", "Id");
            DropColumn("dbo.tab_OAuthTable", "Live");
            DropColumn("dbo.tab_OAuthTable", "Rank");
            DropColumn("dbo.tab_OAuthTable", "Synchronize");
            DropColumn("dbo.tab_OAuthTable", "ModifyDate");
            DropColumn("dbo.tab_OAuthTable", "Remark");
            DropColumn("dbo.tab_OAuthTable", "PictureId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_OAuthTable", "PictureId", c => c.Int());
            AddColumn("dbo.tab_OAuthTable", "Remark", c => c.String(maxLength: 100));
            AddColumn("dbo.tab_OAuthTable", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.tab_OAuthTable", "Synchronize", c => c.Byte(nullable: false));
            AddColumn("dbo.tab_OAuthTable", "Rank", c => c.Byte());
            AddColumn("dbo.tab_OAuthTable", "Live", c => c.Byte(nullable: false));
            DropForeignKey("dbo.tab_OAuthTable", "UserTable_Id", "dbo.tab_UserTable");
            DropIndex("dbo.tab_OAuthTable", new[] { "UserTable_Id" });
            DropColumn("dbo.tab_OAuthTable", "UserTable_Id");
            CreateIndex("dbo.tab_OAuthTable", "UserId");
            AddForeignKey("dbo.tab_OAuthTable", "UserId", "dbo.tab_UserTable", "Id");
        }
    }
}
