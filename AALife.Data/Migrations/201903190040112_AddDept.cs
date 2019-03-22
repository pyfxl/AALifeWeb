namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDept : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tab_UserDeptment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserDeptment", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.UserDeptmentUserTables",
                c => new
                    {
                        UserDeptment_Id = c.Int(nullable: false),
                        UserTable_Id = c.Int(nullable: false),
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
            DropForeignKey("dbo.tab_UserDeptment", "ParentId", "dbo.tab_UserDeptment");
            DropIndex("dbo.UserDeptmentUserTables", new[] { "UserTable_Id" });
            DropIndex("dbo.UserDeptmentUserTables", new[] { "UserDeptment_Id" });
            DropIndex("dbo.tab_UserDeptment", new[] { "ParentId" });
            DropTable("dbo.UserDeptmentUserTables");
            DropTable("dbo.tab_UserDeptment");
        }
    }
}
