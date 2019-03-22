namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDept : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserPermissionUserRoles", newName: "UserRoleUserPermissions");
            DropPrimaryKey("dbo.UserRoleUserPermissions");
            CreateTable(
                "dbo.UserPermissionUserDeptments",
                c => new
                    {
                        UserPermission_Id = c.Int(nullable: false),
                        UserDeptment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserPermission_Id, t.UserDeptment_Id })
                .ForeignKey("dbo.tab_UserPermission", t => t.UserPermission_Id)
                .ForeignKey("dbo.tab_UserDeptment", t => t.UserDeptment_Id)
                .Index(t => t.UserPermission_Id)
                .Index(t => t.UserDeptment_Id);
            
            AddPrimaryKey("dbo.UserRoleUserPermissions", new[] { "UserRole_Id", "UserPermission_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPermissionUserDeptments", "UserDeptment_Id", "dbo.tab_UserDeptment");
            DropForeignKey("dbo.UserPermissionUserDeptments", "UserPermission_Id", "dbo.tab_UserPermission");
            DropIndex("dbo.UserPermissionUserDeptments", new[] { "UserDeptment_Id" });
            DropIndex("dbo.UserPermissionUserDeptments", new[] { "UserPermission_Id" });
            DropPrimaryKey("dbo.UserRoleUserPermissions");
            DropTable("dbo.UserPermissionUserDeptments");
            AddPrimaryKey("dbo.UserRoleUserPermissions", new[] { "UserPermission_Id", "UserRole_Id" });
            RenameTable(name: "dbo.UserRoleUserPermissions", newName: "UserPermissionUserRoles");
        }
    }
}
