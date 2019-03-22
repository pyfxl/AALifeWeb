namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bse_ScheduleTask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Seconds = c.Int(nullable: false),
                        Type = c.String(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                        StopOnError = c.Boolean(nullable: false),
                        LeasedByMachineName = c.String(),
                        LeasedUntilDate = c.DateTime(),
                        LastStartDate = c.DateTime(),
                        LastEndDate = c.DateTime(),
                        LastSuccessDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.bse_ScheduleTask");
        }
    }
}
