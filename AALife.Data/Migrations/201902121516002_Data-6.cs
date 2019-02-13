namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Data6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_UserRole", "Live", c => c.Byte(nullable: false));
            AddColumn("dbo.tab_UserRole", "ModifyDate", c => c.DateTime());
            AddColumn("dbo.tab_UserRole", "Remark", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_UserRole", "Remark");
            DropColumn("dbo.tab_UserRole", "ModifyDate");
            DropColumn("dbo.tab_UserRole", "Live");
        }
    }
}
