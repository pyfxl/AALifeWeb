namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPosition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tab_UserPosition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Notes = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserPositionUserDeptments",
                c => new
                    {
                        UserPosition_Id = c.Int(nullable: false),
                        UserDeptment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserPosition_Id, t.UserDeptment_Id })
                .ForeignKey("dbo.tab_UserPosition", t => t.UserPosition_Id)
                .ForeignKey("dbo.tab_UserDeptment", t => t.UserDeptment_Id)
                .Index(t => t.UserPosition_Id)
                .Index(t => t.UserDeptment_Id);
            
            CreateTable(
                "dbo.UserPositionUserPermissions",
                c => new
                    {
                        UserPosition_Id = c.Int(nullable: false),
                        UserPermission_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserPosition_Id, t.UserPermission_Id })
                .ForeignKey("dbo.tab_UserPosition", t => t.UserPosition_Id)
                .ForeignKey("dbo.tab_UserPermission", t => t.UserPermission_Id)
                .Index(t => t.UserPosition_Id)
                .Index(t => t.UserPermission_Id);
            
            CreateTable(
                "dbo.UserPositionUserTables",
                c => new
                    {
                        UserPosition_Id = c.Int(nullable: false),
                        UserTable_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserPosition_Id, t.UserTable_Id })
                .ForeignKey("dbo.tab_UserPosition", t => t.UserPosition_Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserTable_Id)
                .Index(t => t.UserPosition_Id)
                .Index(t => t.UserTable_Id);
            
            AddColumn("dbo.tab_UserDeptment", "Notes", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPositionUserTables", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.UserPositionUserTables", "UserPosition_Id", "dbo.tab_UserPosition");
            DropForeignKey("dbo.UserPositionUserPermissions", "UserPermission_Id", "dbo.tab_UserPermission");
            DropForeignKey("dbo.UserPositionUserPermissions", "UserPosition_Id", "dbo.tab_UserPosition");
            DropForeignKey("dbo.UserPositionUserDeptments", "UserDeptment_Id", "dbo.tab_UserDeptment");
            DropForeignKey("dbo.UserPositionUserDeptments", "UserPosition_Id", "dbo.tab_UserPosition");
            DropIndex("dbo.UserPositionUserTables", new[] { "UserTable_Id" });
            DropIndex("dbo.UserPositionUserTables", new[] { "UserPosition_Id" });
            DropIndex("dbo.UserPositionUserPermissions", new[] { "UserPermission_Id" });
            DropIndex("dbo.UserPositionUserPermissions", new[] { "UserPosition_Id" });
            DropIndex("dbo.UserPositionUserDeptments", new[] { "UserDeptment_Id" });
            DropIndex("dbo.UserPositionUserDeptments", new[] { "UserPosition_Id" });
            DropColumn("dbo.tab_UserDeptment", "Notes");
            DropTable("dbo.UserPositionUserTables");
            DropTable("dbo.UserPositionUserPermissions");
            DropTable("dbo.UserPositionUserDeptments");
            DropTable("dbo.tab_UserPosition");
        }
    }
}
