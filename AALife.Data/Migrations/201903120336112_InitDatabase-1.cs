namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tab_Permission", newName: "PermissionRecords");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PermissionRecords", newName: "tab_Permission");
        }
    }
}
