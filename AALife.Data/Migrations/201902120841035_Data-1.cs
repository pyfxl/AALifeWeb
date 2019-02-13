namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Data1 : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.tab_UserTable", newName: "UserTables");
            //CreateTable(
            //    "dbo.UserRoles",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            IsSystemRole = c.Boolean(nullable: false),
            //            SystemName = c.String(),
            //            UserTable_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.UserTables", t => t.UserTable_Id)
            //    .Index(t => t.UserTable_Id);
            
            //CreateTable(
            //    "dbo.PermissionRecords",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            SystemName = c.String(),
            //            Category = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.PermissionRecordUserRoles",
            //    c => new
            //        {
            //            PermissionRecord_Id = c.Int(nullable: false),
            //            UserRole_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.PermissionRecord_Id, t.UserRole_Id })
            //    .ForeignKey("dbo.PermissionRecords", t => t.PermissionRecord_Id)
            //    .ForeignKey("dbo.UserRoles", t => t.UserRole_Id)
            //    .Index(t => t.PermissionRecord_Id)
            //    .Index(t => t.UserRole_Id);
            
            //AlterColumn("dbo.UserTables", "UserName", c => c.String());
            //AlterColumn("dbo.UserTables", "UserPassword", c => c.String());
            //AlterColumn("dbo.UserTables", "PasswordSalt", c => c.String());
            //AlterColumn("dbo.UserTables", "UserNickName", c => c.String());
            //AlterColumn("dbo.UserTables", "UserEmail", c => c.String());
            //AlterColumn("dbo.UserTables", "UserImage", c => c.String());
            //AlterColumn("dbo.UserTables", "UserTheme", c => c.String());
            //AlterColumn("dbo.UserTables", "UserFrom", c => c.String());
            //AlterColumn("dbo.UserTables", "Synchronize", c => c.Int(nullable: false));
            //DropColumn("dbo.UserTables", "Live");
            //DropColumn("dbo.UserTables", "Remark");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserTables", "Remark", c => c.String(maxLength: 100));
            AddColumn("dbo.UserTables", "Live", c => c.Byte(nullable: false));
            DropForeignKey("dbo.UserRoles", "UserTable_Id", "dbo.UserTables");
            DropForeignKey("dbo.PermissionRecordUserRoles", "UserRole_Id", "dbo.UserRoles");
            DropForeignKey("dbo.PermissionRecordUserRoles", "PermissionRecord_Id", "dbo.PermissionRecords");
            DropIndex("dbo.PermissionRecordUserRoles", new[] { "UserRole_Id" });
            DropIndex("dbo.PermissionRecordUserRoles", new[] { "PermissionRecord_Id" });
            DropIndex("dbo.UserRoles", new[] { "UserTable_Id" });
            AlterColumn("dbo.UserTables", "Synchronize", c => c.Byte(nullable: false));
            AlterColumn("dbo.UserTables", "UserFrom", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.UserTables", "UserTheme", c => c.String(maxLength: 10));
            AlterColumn("dbo.UserTables", "UserImage", c => c.String(maxLength: 200));
            AlterColumn("dbo.UserTables", "UserEmail", c => c.String(maxLength: 200));
            AlterColumn("dbo.UserTables", "UserNickName", c => c.String(maxLength: 50));
            AlterColumn("dbo.UserTables", "PasswordSalt", c => c.String(maxLength: 10));
            AlterColumn("dbo.UserTables", "UserPassword", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.UserTables", "UserName", c => c.String(nullable: false, maxLength: 20));
            DropTable("dbo.PermissionRecordUserRoles");
            DropTable("dbo.PermissionRecords");
            DropTable("dbo.UserRoles");
            RenameTable(name: "dbo.UserTables", newName: "tab_UserTable");
        }
    }
}
