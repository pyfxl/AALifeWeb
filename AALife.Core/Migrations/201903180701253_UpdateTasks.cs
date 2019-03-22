namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.bse_ScheduleTask", "SystemName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.bse_ScheduleTask", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.bse_ScheduleTask", "Type", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.bse_ScheduleTask", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.bse_ScheduleTask", "Name", c => c.String(nullable: false));
            DropColumn("dbo.bse_ScheduleTask", "SystemName");
        }
    }
}
