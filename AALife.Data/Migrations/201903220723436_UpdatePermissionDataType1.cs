namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermissionDataType1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_UserPermission", "PermissionType", c => c.String(maxLength: 20));
            DropColumn("dbo.tab_UserPermission", "DataType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_UserPermission", "DataType", c => c.String(maxLength: 20));
            DropColumn("dbo.tab_UserPermission", "PermissionType");
        }
    }
}
