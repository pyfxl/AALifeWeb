namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewPicture : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.bse_Picture");
        }
    }
}
