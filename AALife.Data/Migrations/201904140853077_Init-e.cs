namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inite : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tab_UserPosition", "Name", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tab_UserPosition", "Name", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
