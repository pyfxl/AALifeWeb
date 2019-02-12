namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.bse_CustomerRole", "SystemName", c => c.String(maxLength: 20));
            AlterColumn("dbo.bse_PermissionRecord", "SystemName", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.bse_PermissionRecord", "SystemName", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.bse_CustomerRole", "SystemName", c => c.String(maxLength: 10));
        }
    }
}
