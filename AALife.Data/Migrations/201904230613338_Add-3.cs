namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPermissionUserDeptments",
                c => new
                    {
                        UserPermission_Id = c.Guid(nullable: false),
                        UserDeptment_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserPermission_Id, t.UserDeptment_Id })
                .ForeignKey("dbo.usr_UserPermission", t => t.UserPermission_Id)
                .ForeignKey("dbo.usr_UserDeptment", t => t.UserDeptment_Id)
                .Index(t => t.UserPermission_Id)
                .Index(t => t.UserDeptment_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPermissionUserDeptments", "UserDeptment_Id", "dbo.usr_UserDeptment");
            DropForeignKey("dbo.UserPermissionUserDeptments", "UserPermission_Id", "dbo.usr_UserPermission");
            DropIndex("dbo.UserPermissionUserDeptments", new[] { "UserDeptment_Id" });
            DropIndex("dbo.UserPermissionUserDeptments", new[] { "UserPermission_Id" });
            DropTable("dbo.UserPermissionUserDeptments");
        }
    }
}
