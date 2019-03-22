namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaska : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.bse_QueuedEmail", "PriorityId", c => c.Int(nullable: false));
            AddColumn("dbo.bse_QueuedEmail", "SentTries", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.bse_QueuedEmail", "SentTries");
            DropColumn("dbo.bse_QueuedEmail", "PriorityId");
        }
    }
}
