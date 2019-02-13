namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Core1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bse_ActivityLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityLogTypeId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
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
                        UserId = c.Int(),
                        PageUrl = c.String(),
                        ReferrerUrl = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.bse_Setting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Value = c.String(nullable: false, maxLength: 2000),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.bse_Setting");
            DropTable("dbo.bse_Picture");
            DropTable("dbo.bse_Log");
            DropTable("dbo.bse_ActivityLog");
        }
    }
}
