namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Data5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tab_UserRole", "Name", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.tab_UserRole", "SystemName", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tab_UserRole", "SystemName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.tab_UserRole", "Name", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
