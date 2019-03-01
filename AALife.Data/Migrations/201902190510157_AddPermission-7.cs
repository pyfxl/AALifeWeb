namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_Permission", "Rank", c => c.Byte());
            AddColumn("dbo.tab_Permission", "ModifyDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_Permission", "ModifyDate");
            DropColumn("dbo.tab_Permission", "Rank");
        }
    }
}
