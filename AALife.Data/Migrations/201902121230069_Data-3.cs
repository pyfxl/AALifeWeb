namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Data3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_UserTable", "Live", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_UserTable", "Live");
        }
    }
}
