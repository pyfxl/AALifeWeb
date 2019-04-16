namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inite : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserDeptmentUserTables", "UserDeptment_Id", "dbo.tab_UserDeptment");
            DropForeignKey("dbo.UserDeptmentUserTables", "UserTable_Id", "dbo.tab_UserTable");
            DropIndex("dbo.UserDeptmentUserTables", new[] { "UserDeptment_Id" });
            DropIndex("dbo.UserDeptmentUserTables", new[] { "UserTable_Id" });
            DropColumn("dbo.tab_UserDeptment", "Category");
            DropTable("dbo.UserDeptmentUserTables");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserDeptmentUserTables",
                c => new
                    {
                        UserDeptment_Id = c.Guid(nullable: false),
                        UserTable_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserDeptment_Id, t.UserTable_Id });
            
            AddColumn("dbo.tab_UserDeptment", "Category", c => c.String(maxLength: 20));
            CreateIndex("dbo.UserDeptmentUserTables", "UserTable_Id");
            CreateIndex("dbo.UserDeptmentUserTables", "UserDeptment_Id");
            AddForeignKey("dbo.UserDeptmentUserTables", "UserTable_Id", "dbo.tab_UserTable", "Id");
            AddForeignKey("dbo.UserDeptmentUserTables", "UserDeptment_Id", "dbo.tab_UserDeptment", "Id");
        }
    }
}
