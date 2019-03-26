namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIconName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_UserPermission", "IconName", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_UserPermission", "IconName");
        }
    }
}
