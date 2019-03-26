namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermissionDataType2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_UserPermission", "IsButton", c => c.Boolean(nullable: false));
            DropColumn("dbo.tab_UserPermission", "PermissionType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_UserPermission", "PermissionType", c => c.String(maxLength: 20));
            DropColumn("dbo.tab_UserPermission", "IsButton");
        }
    }
}
