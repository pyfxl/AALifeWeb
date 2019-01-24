namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tab_CardTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardId = c.Int(nullable: false),
                        CardName = c.String(nullable: false, maxLength: 20),
                        CardNumber = c.String(maxLength: 50),
                        MoneyStart = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(nullable: false),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                        Image = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tab_UserTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 20),
                        UserPassword = c.String(nullable: false, maxLength: 20),
                        UserNickName = c.String(maxLength: 50),
                        UserEmail = c.String(maxLength: 200),
                        UserImage = c.String(maxLength: 200),
                        UserTheme = c.String(maxLength: 10),
                        UserLevel = c.Byte(nullable: false),
                        UserFrom = c.String(nullable: false, maxLength: 10),
                        CreateDate = c.DateTime(nullable: false),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                        Image = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tab_CategoryTypeTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryTypeName = c.String(nullable: false, maxLength: 20),
                        CategoryTypeId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                        Image = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tab_ItemTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false, maxLength: 20),
                        CategoryTypeId = c.Int(nullable: false),
                        ItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemBuyDate = c.DateTime(nullable: false),
                        Recommend = c.Byte(),
                        ItemAppId = c.Int(),
                        RegionId = c.Int(),
                        RegionType = c.String(maxLength: 10),
                        ItemType = c.String(nullable: false, maxLength: 10),
                        ZhuanTiId = c.Int(),
                        CardId = c.Int(nullable: false),
                        Remark = c.String(maxLength: 100),
                        UserId = c.Int(nullable: false),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                        Image = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tab_OAuthTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OpenId = c.String(nullable: false, maxLength: 100),
                        AccessToken = c.String(nullable: false, maxLength: 100),
                        OldUserId = c.Int(nullable: false),
                        OAuthBound = c.Int(nullable: false),
                        OAuthFrom = c.String(maxLength: 10),
                        UserId = c.Int(nullable: false),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                        Image = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tab_ZhuanTiTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZhuanTiName = c.String(nullable: false, maxLength: 20),
                        ZhuanTiId = c.Int(),
                        UserId = c.Int(nullable: false),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                        Image = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tab_UserFromTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserFrom = c.String(maxLength: 10),
                        UserFromName = c.String(nullable: false, maxLength: 10),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                        Image = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tab_UserLevelTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserLevel = c.Byte(nullable: false),
                        UserLevelName = c.String(nullable: false, maxLength: 10),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                        Image = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tab_ZhuanTiTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_OAuthTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ItemTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_CategoryTypeTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_CardTable", "UserId", "dbo.tab_UserTable");
            DropIndex("dbo.tab_ZhuanTiTable", new[] { "UserId" });
            DropIndex("dbo.tab_OAuthTable", new[] { "UserId" });
            DropIndex("dbo.tab_ItemTable", new[] { "UserId" });
            DropIndex("dbo.tab_CategoryTypeTable", new[] { "UserId" });
            DropIndex("dbo.tab_CardTable", new[] { "UserId" });
            DropTable("dbo.tab_UserLevelTable");
            DropTable("dbo.tab_UserFromTable");
            DropTable("dbo.tab_ZhuanTiTable");
            DropTable("dbo.tab_OAuthTable");
            DropTable("dbo.tab_ItemTable");
            DropTable("dbo.tab_CategoryTypeTable");
            DropTable("dbo.tab_UserTable");
            DropTable("dbo.tab_CardTable");
        }
    }
}
