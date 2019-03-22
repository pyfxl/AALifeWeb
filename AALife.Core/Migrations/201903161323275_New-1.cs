namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New1 : DbMigration
    {
        public override void Up()
        {
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
                "dbo.bse_QueuedEmail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.String(nullable: false, maxLength: 200),
                        FromName = c.String(maxLength: 20),
                        To = c.String(nullable: false, maxLength: 200),
                        ToName = c.String(maxLength: 20),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Body = c.String(nullable: false, maxLength: 1000),
                        CreatedDate = c.DateTime(nullable: false),
                        SentDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.bse_QueuedEmail");
            DropTable("dbo.bse_MessageTemplate");
        }
    }
}
