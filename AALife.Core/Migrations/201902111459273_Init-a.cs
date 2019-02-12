namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inita : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bse_ActivityLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityLogTypeId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Comment = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ActivityLogType = c.Int(nullable: false),
                        IpAddress = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.bse_Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.bse_Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 20),
                        Email = c.String(maxLength: 200),
                        Password = c.String(maxLength: 50),
                        PasswordSalt = c.String(maxLength: 10),
                        ResetPassword = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        LastIpAddress = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastLoginDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.bse_CustomerRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Active = c.Boolean(nullable: false),
                        IsSystemRole = c.Boolean(nullable: false),
                        SystemName = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.bse_PermissionRecord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        SystemName = c.String(nullable: false, maxLength: 10),
                        Category = c.String(nullable: false, maxLength: 10),
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
                        CustomerId = c.Int(),
                        PageUrl = c.String(),
                        ReferrerUrl = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.bse_Customer", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
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
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.bse_Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.bse_Setting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Value = c.String(nullable: false, maxLength: 2000),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.bse_PermissionRecord_Role_Mapping",
                c => new
                    {
                        PermissionRecord_Id = c.Int(nullable: false),
                        CustomerRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionRecord_Id, t.CustomerRole_Id })
                .ForeignKey("dbo.bse_PermissionRecord", t => t.PermissionRecord_Id, cascadeDelete: true)
                .ForeignKey("dbo.bse_CustomerRole", t => t.CustomerRole_Id, cascadeDelete: true)
                .Index(t => t.PermissionRecord_Id)
                .Index(t => t.CustomerRole_Id);
            
            CreateTable(
                "dbo.bse_Customer_CustomerRole_Mapping",
                c => new
                    {
                        Customer_Id = c.Int(nullable: false),
                        CustomerRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Customer_Id, t.CustomerRole_Id })
                .ForeignKey("dbo.bse_Customer", t => t.Customer_Id, cascadeDelete: true)
                .ForeignKey("dbo.bse_CustomerRole", t => t.CustomerRole_Id, cascadeDelete: true)
                .Index(t => t.Customer_Id)
                .Index(t => t.CustomerRole_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.bse_Picture", "CustomerId", "dbo.bse_Customer");
            DropForeignKey("dbo.bse_Log", "CustomerId", "dbo.bse_Customer");
            DropForeignKey("dbo.bse_ActivityLog", "CustomerId", "dbo.bse_Customer");
            DropForeignKey("dbo.bse_Customer_CustomerRole_Mapping", "CustomerRole_Id", "dbo.bse_CustomerRole");
            DropForeignKey("dbo.bse_Customer_CustomerRole_Mapping", "Customer_Id", "dbo.bse_Customer");
            DropForeignKey("dbo.bse_PermissionRecord_Role_Mapping", "CustomerRole_Id", "dbo.bse_CustomerRole");
            DropForeignKey("dbo.bse_PermissionRecord_Role_Mapping", "PermissionRecord_Id", "dbo.bse_PermissionRecord");
            DropIndex("dbo.bse_Customer_CustomerRole_Mapping", new[] { "CustomerRole_Id" });
            DropIndex("dbo.bse_Customer_CustomerRole_Mapping", new[] { "Customer_Id" });
            DropIndex("dbo.bse_PermissionRecord_Role_Mapping", new[] { "CustomerRole_Id" });
            DropIndex("dbo.bse_PermissionRecord_Role_Mapping", new[] { "PermissionRecord_Id" });
            DropIndex("dbo.bse_Picture", new[] { "CustomerId" });
            DropIndex("dbo.bse_Log", new[] { "CustomerId" });
            DropIndex("dbo.bse_ActivityLog", new[] { "CustomerId" });
            DropTable("dbo.bse_Customer_CustomerRole_Mapping");
            DropTable("dbo.bse_PermissionRecord_Role_Mapping");
            DropTable("dbo.bse_Setting");
            DropTable("dbo.bse_Picture");
            DropTable("dbo.bse_Log");
            DropTable("dbo.bse_PermissionRecord");
            DropTable("dbo.bse_CustomerRole");
            DropTable("dbo.bse_Customer");
            DropTable("dbo.bse_ActivityLog");
        }
    }
}
