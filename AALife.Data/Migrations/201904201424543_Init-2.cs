namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tab_UserTable", newName: "usr_UserTable");
            RenameTable(name: "dbo.tab_UserDeptment", newName: "usr_UserDeptment");
            RenameTable(name: "dbo.tab_UserPosition", newName: "usr_UserPosition");
            RenameTable(name: "dbo.tab_UserPermission", newName: "usr_UserPermission");
            RenameTable(name: "dbo.tab_UserRole", newName: "usr_UserRole");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.usr_UserRole", newName: "tab_UserRole");
            RenameTable(name: "dbo.usr_UserPermission", newName: "tab_UserPermission");
            RenameTable(name: "dbo.usr_UserPosition", newName: "tab_UserPosition");
            RenameTable(name: "dbo.usr_UserDeptment", newName: "tab_UserDeptment");
            RenameTable(name: "dbo.usr_UserTable", newName: "tab_UserTable");
        }
    }
}
