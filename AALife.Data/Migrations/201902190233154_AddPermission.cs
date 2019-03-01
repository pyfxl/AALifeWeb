namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tab_Permission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                        SystemName = c.String(nullable: false, maxLength: 20),
                        Category = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionRecordUserRoles",
                c => new
                    {
                        PermissionRecord_Id = c.Int(nullable: false),
                        UserRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionRecord_Id, t.UserRole_Id })
                .ForeignKey("dbo.tab_Permission", t => t.PermissionRecord_Id)
                .ForeignKey("dbo.tab_UserRole", t => t.UserRole_Id)
                .Index(t => t.PermissionRecord_Id)
                .Index(t => t.UserRole_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermissionRecordUserRoles", "UserRole_Id", "dbo.tab_UserRole");
            DropForeignKey("dbo.PermissionRecordUserRoles", "PermissionRecord_Id", "dbo.tab_Permission");
            DropIndex("dbo.PermissionRecordUserRoles", new[] { "UserRole_Id" });
            DropIndex("dbo.PermissionRecordUserRoles", new[] { "PermissionRecord_Id" });
            DropTable("dbo.PermissionRecordUserRoles");
            DropTable("dbo.tab_Permission");
        }
    }
}
