namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Data4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tab_UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        IsSystemRole = c.Boolean(nullable: false),
                        SystemName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoleUserTables",
                c => new
                    {
                        UserRole_Id = c.Int(nullable: false),
                        UserTable_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRole_Id, t.UserTable_Id })
                .ForeignKey("dbo.tab_UserRole", t => t.UserRole_Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserTable_Id)
                .Index(t => t.UserRole_Id)
                .Index(t => t.UserTable_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoleUserTables", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.UserRoleUserTables", "UserRole_Id", "dbo.tab_UserRole");
            DropIndex("dbo.UserRoleUserTables", new[] { "UserTable_Id" });
            DropIndex("dbo.UserRoleUserTables", new[] { "UserRole_Id" });
            DropTable("dbo.UserRoleUserTables");
            DropTable("dbo.tab_UserRole");
        }
    }
}
