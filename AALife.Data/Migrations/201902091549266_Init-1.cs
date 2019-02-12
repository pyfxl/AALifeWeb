namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tab_CardTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardName = c.String(nullable: false, maxLength: 20),
                        CardNumber = c.String(maxLength: 50),
                        MoneyStart = c.Decimal(precision: 18, scale: 2),
                        CardId = c.Int(),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(),
                        Remark = c.String(maxLength: 100),
                        Image = c.String(maxLength: 200),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tab_UserTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 20),
                        UserPassword = c.String(nullable: false, maxLength: 50),
                        PasswordSalt = c.String(maxLength: 10),
                        UserNickName = c.String(maxLength: 50),
                        UserEmail = c.String(maxLength: 200),
                        UserImage = c.String(maxLength: 200),
                        UserTheme = c.String(maxLength: 10),
                        UserLevel = c.Byte(nullable: false),
                        UserFrom = c.String(nullable: false, maxLength: 10),
                        CreateDate = c.DateTime(nullable: false),
                        Live = c.Byte(nullable: false),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(),
                        Remark = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tab_CategoryTypeTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryTypeName = c.String(nullable: false, maxLength: 20),
                        CategoryTypeId = c.Int(),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(),
                        Remark = c.String(maxLength: 100),
                        Image = c.String(maxLength: 200),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tab_ItemTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false, maxLength: 20),
                        ItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemBuyDate = c.DateTime(nullable: false),
                        ItemAppId = c.Int(),
                        Recommend = c.Byte(),
                        RegionId = c.Int(),
                        RegionType = c.String(maxLength: 10),
                        ItemType = c.String(nullable: false, maxLength: 10),
                        CategoryTypeId = c.Int(),
                        ZhuanTiId = c.Int(),
                        CardId = c.Int(),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(),
                        Remark = c.String(maxLength: 100),
                        Image = c.String(maxLength: 200),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId)
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
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(),
                        Remark = c.String(maxLength: 100),
                        Image = c.String(maxLength: 200),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tab_ZhuanTiTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZhuanTiName = c.String(nullable: false, maxLength: 20),
                        ZhuanTiId = c.Int(),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(),
                        Remark = c.String(maxLength: 100),
                        Image = c.String(maxLength: 200),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tab_ZhuanZhangTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZhuanZhangFrom = c.Int(nullable: false),
                        ZhuanZhangTo = c.Int(nullable: false),
                        ZhuanZhangDate = c.DateTime(nullable: false),
                        ZhuanZhangMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ZhuanZhangId = c.Int(),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(),
                        Remark = c.String(maxLength: 100),
                        Image = c.String(maxLength: 200),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tab_ZhuanZhangTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ZhuanTiTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_OAuthTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ItemTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_CategoryTypeTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_CardTable", "UserId", "dbo.tab_UserTable");
            DropIndex("dbo.tab_ZhuanZhangTable", new[] { "UserId" });
            DropIndex("dbo.tab_ZhuanTiTable", new[] { "UserId" });
            DropIndex("dbo.tab_OAuthTable", new[] { "UserId" });
            DropIndex("dbo.tab_ItemTable", new[] { "UserId" });
            DropIndex("dbo.tab_CategoryTypeTable", new[] { "UserId" });
            DropIndex("dbo.tab_CardTable", new[] { "UserId" });
            DropTable("dbo.tab_ZhuanZhangTable");
            DropTable("dbo.tab_ZhuanTiTable");
            DropTable("dbo.tab_OAuthTable");
            DropTable("dbo.tab_ItemTable");
            DropTable("dbo.tab_CategoryTypeTable");
            DropTable("dbo.tab_UserTable");
            DropTable("dbo.tab_CardTable");
        }
    }
}
