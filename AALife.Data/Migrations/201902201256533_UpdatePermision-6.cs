namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermision6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tab_UserTable", "Live");
            DropColumn("dbo.tab_UserTable", "Remark");
            DropColumn("dbo.tab_UserRole", "IsSystemRole");
            DropColumn("dbo.tab_UserRole", "Live");
            DropColumn("dbo.tab_UserRole", "ModifyDate");
            DropColumn("dbo.tab_UserRole", "Remark");
            DropColumn("dbo.tab_Permission", "Live");
            DropColumn("dbo.tab_Permission", "ModifyDate");
            DropColumn("dbo.tab_Permission", "Remark");
            DropColumn("dbo.tab_Permission", "SystemName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_Permission", "SystemName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.tab_Permission", "Remark", c => c.String(maxLength: 100));
            AddColumn("dbo.tab_Permission", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.tab_Permission", "Live", c => c.Byte(nullable: false));
            AddColumn("dbo.tab_UserRole", "Remark", c => c.String(maxLength: 100));
            AddColumn("dbo.tab_UserRole", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.tab_UserRole", "Live", c => c.Byte(nullable: false));
            AddColumn("dbo.tab_UserRole", "IsSystemRole", c => c.Boolean(nullable: false));
            AddColumn("dbo.tab_UserTable", "Remark", c => c.String(maxLength: 100));
            AddColumn("dbo.tab_UserTable", "Live", c => c.Byte(nullable: false));
        }
    }
}
