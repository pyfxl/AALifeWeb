namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Core2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.bse_ActivityLog", "UserId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.bse_ActivityLog", "UserId", c => c.Int(nullable: false));
        }
    }
}
