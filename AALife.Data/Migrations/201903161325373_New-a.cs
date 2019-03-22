namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newa : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PermissionRecords", newName: "tab_UserPermission");
            RenameTable(name: "dbo.PermissionRecordUserRoles", newName: "UserPermissionUserRoles");
            RenameColumn(table: "dbo.UserPermissionUserRoles", name: "PermissionRecord_Id", newName: "UserPermission_Id");
            RenameIndex(table: "dbo.UserPermissionUserRoles", name: "IX_PermissionRecord_Id", newName: "IX_UserPermission_Id");
            AlterColumn("dbo.tab_UserPermission", "ControllerName", c => c.String(maxLength: 20));
            AlterColumn("dbo.tab_UserPermission", "ActionName", c => c.String(maxLength: 20));
            DropTable("dbo.Employees");
            DropTable("dbo.MessageTemplates");
            DropTable("dbo.QueuedEmails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QueuedEmails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.String(nullable: false, maxLength: 200),
                        FromName = c.String(maxLength: 20),
                        To = c.String(nullable: false, maxLength: 200),
                        ToName = c.String(maxLength: 20),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Body = c.String(nullable: false, maxLength: 1000),
                        CreatedDate = c.DateTime(nullable: false),
                        SentDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessageTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        SystemName = c.String(nullable: false, maxLength: 20),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Body = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Designation = c.String(maxLength: 50),
                        ReportTo = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            AlterColumn("dbo.tab_UserPermission", "ActionName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.tab_UserPermission", "ControllerName", c => c.String(nullable: false, maxLength: 20));
            RenameIndex(table: "dbo.UserPermissionUserRoles", name: "IX_UserPermission_Id", newName: "IX_PermissionRecord_Id");
            RenameColumn(table: "dbo.UserPermissionUserRoles", name: "UserPermission_Id", newName: "PermissionRecord_Id");
            RenameTable(name: "dbo.UserPermissionUserRoles", newName: "PermissionRecordUserRoles");
            RenameTable(name: "dbo.tab_UserPermission", newName: "PermissionRecords");
        }
    }
}
