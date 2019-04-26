namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPermissionUserPositions",
                c => new
                    {
                        UserPermission_Id = c.Guid(nullable: false),
                        UserPosition_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserPermission_Id, t.UserPosition_Id })
                .ForeignKey("dbo.usr_UserPermission", t => t.UserPermission_Id)
                .ForeignKey("dbo.usr_UserPosition", t => t.UserPosition_Id)
                .Index(t => t.UserPermission_Id)
                .Index(t => t.UserPosition_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPermissionUserPositions", "UserPosition_Id", "dbo.usr_UserPosition");
            DropForeignKey("dbo.UserPermissionUserPositions", "UserPermission_Id", "dbo.usr_UserPermission");
            DropIndex("dbo.UserPermissionUserPositions", new[] { "UserPosition_Id" });
            DropIndex("dbo.UserPermissionUserPositions", new[] { "UserPermission_Id" });
            DropTable("dbo.UserPermissionUserPositions");
        }
    }
}
