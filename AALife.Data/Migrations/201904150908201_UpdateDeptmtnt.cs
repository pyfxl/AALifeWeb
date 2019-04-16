namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDeptmtnt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDeptmentUserTables",
                c => new
                    {
                        UserDeptment_Id = c.Guid(nullable: false),
                        UserTable_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserDeptment_Id, t.UserTable_Id })
                .ForeignKey("dbo.tab_UserDeptment", t => t.UserDeptment_Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserTable_Id)
                .Index(t => t.UserDeptment_Id)
                .Index(t => t.UserTable_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDeptmentUserTables", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.UserDeptmentUserTables", "UserDeptment_Id", "dbo.tab_UserDeptment");
            DropIndex("dbo.UserDeptmentUserTables", new[] { "UserTable_Id" });
            DropIndex("dbo.UserDeptmentUserTables", new[] { "UserDeptment_Id" });
            DropTable("dbo.UserDeptmentUserTables");
        }
    }
}
