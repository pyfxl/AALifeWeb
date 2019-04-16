namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bse_ActivityLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityLogTypeId = c.Int(nullable: false),
                        UserId = c.Guid(),
                        Comment = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ActivityLogType = c.Int(nullable: false),
                        IpAddress = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.bse_Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogLevelId = c.Int(nullable: false),
                        ShortMessage = c.String(nullable: false),
                        FullMessage = c.String(),
                        IpAddress = c.String(maxLength: 200),
                        UserId = c.Guid(),
                        PageUrl = c.String(),
                        ReferrerUrl = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.bse_MessageTemplate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        SystemName = c.String(nullable: false, maxLength: 20),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Body = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.bse_Parameter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Value = c.String(nullable: false, maxLength: 20),
                        ParentId = c.Int(),
                        IsLeaf = c.Boolean(),
                        IsDefault = c.Boolean(),
                        IsSystem = c.Boolean(),
                        Notes = c.String(maxLength: 200),
                        Rank = c.Byte(),
                        OrderNo = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.bse_Parameter", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.bse_Picture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MimeType = c.String(nullable: false, maxLength: 40),
                        IsNew = c.Boolean(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 100),
                        FileExtName = c.String(nullable: false, maxLength: 10),
                        FileBytes = c.Int(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.bse_QueuedEmail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriorityId = c.Int(nullable: false),
                        From = c.String(nullable: false, maxLength: 200),
                        FromName = c.String(maxLength: 20),
                        To = c.String(nullable: false, maxLength: 200),
                        ToName = c.String(maxLength: 20),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Body = c.String(nullable: false, maxLength: 1000),
                        CreatedDate = c.DateTime(nullable: false),
                        SentDate = c.DateTime(),
                        SentTries = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.bse_ScheduleTask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        SystemName = c.String(nullable: false, maxLength: 20),
                        Seconds = c.Int(nullable: false),
                        Type = c.String(nullable: false, maxLength: 200),
                        Enabled = c.Boolean(nullable: false),
                        StopOnError = c.Boolean(nullable: false),
                        LastStartDate = c.DateTime(),
                        LastEndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.bse_Setting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Value = c.String(nullable: false, maxLength: 2000),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.bse_Parameter", "ParentId", "dbo.bse_Parameter");
            DropIndex("dbo.bse_Parameter", new[] { "ParentId" });
            DropTable("dbo.bse_Setting");
            DropTable("dbo.bse_ScheduleTask");
            DropTable("dbo.bse_QueuedEmail");
            DropTable("dbo.bse_Picture");
            DropTable("dbo.bse_Parameter");
            DropTable("dbo.bse_MessageTemplate");
            DropTable("dbo.bse_Log");
            DropTable("dbo.bse_ActivityLog");
        }
    }
}
