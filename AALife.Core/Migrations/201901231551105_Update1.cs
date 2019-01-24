namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.tab_UserTable", "UserFromTable_Id", c => c.Int());
            //AddColumn("dbo.tab_UserTable", "UserLevelTable_Id", c => c.Int());
            //CreateIndex("dbo.tab_UserTable", "UserFromTable_Id");
            //CreateIndex("dbo.tab_UserTable", "UserLevelTable_Id");
            //AddForeignKey("dbo.tab_UserTable", "UserFromTable_Id", "dbo.tab_UserFromTable", "Id");
            //AddForeignKey("dbo.tab_UserTable", "UserLevelTable_Id", "dbo.tab_UserLevelTable", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tab_UserTable", "UserLevelTable_Id", "dbo.tab_UserLevelTable");
            DropForeignKey("dbo.tab_UserTable", "UserFromTable_Id", "dbo.tab_UserFromTable");
            DropIndex("dbo.tab_UserTable", new[] { "UserLevelTable_Id" });
            DropIndex("dbo.tab_UserTable", new[] { "UserFromTable_Id" });
            DropColumn("dbo.tab_UserTable", "UserLevelTable_Id");
            DropColumn("dbo.tab_UserTable", "UserFromTable_Id");
        }
    }
}
