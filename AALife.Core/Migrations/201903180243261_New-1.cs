namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.bse_ScheduleTask", "LeasedByMachineName");
            DropColumn("dbo.bse_ScheduleTask", "LeasedUntilDate");
            DropColumn("dbo.bse_ScheduleTask", "LastSuccessDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.bse_ScheduleTask", "LastSuccessDate", c => c.DateTime());
            AddColumn("dbo.bse_ScheduleTask", "LeasedUntilDate", c => c.DateTime());
            AddColumn("dbo.bse_ScheduleTask", "LeasedByMachineName", c => c.String());
        }
    }
}
