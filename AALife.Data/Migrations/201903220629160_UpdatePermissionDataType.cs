namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermissionDataType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_UserPermission", "DataType", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_UserPermission", "DataType");
        }
    }
}
