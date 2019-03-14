namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
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
                        PictureId = c.Int(),
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
                        Remark = c.String(maxLength: 100),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(),
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
                        PictureId = c.Int(),
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
                        CategoryTypeSyncId = c.Int(),
                        ZhuanTiSyncId = c.Int(),
                        CardSyncId = c.Int(),
                        CategoryTypeId = c.Int(),
                        ZhuanTiId = c.Int(),
                        CardId = c.Int(),
                        Live = c.Byte(nullable: false),
                        Rank = c.Byte(),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(),
                        Remark = c.String(maxLength: 100),
                        PictureId = c.Int(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_CardTable", t => t.CardId)
                .ForeignKey("dbo.tab_CategoryTypeTable", t => t.CategoryTypeId)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId)
                .ForeignKey("dbo.tab_ZhuanTiTable", t => t.ZhuanTiId)
                .Index(t => t.CategoryTypeId)
                .Index(t => t.ZhuanTiId)
                .Index(t => t.CardId)
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
                        PictureId = c.Int(),
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
                        UserId = c.Int(nullable: false),
                        OldUserId = c.Int(nullable: false),
                        OAuthBound = c.Int(nullable: false),
                        OAuthFrom = c.String(maxLength: 10),
                        UserTable_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserTable_Id)
                .Index(t => t.UserTable_Id);
            
            CreateTable(
                "dbo.tab_UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        SystemName = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tab_Permission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        AreaName = c.String(maxLength: 20),
                        ControllerName = c.String(nullable: false, maxLength: 20),
                        ActionName = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 20),
                        Rank = c.Byte(),
                        OrderNo = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_Permission", t => t.ParentId)
                .Index(t => t.ParentId);
            
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
                        PictureId = c.Int(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Designation = c.String(maxLength: 50),
                        ReportTo = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.MessageTemplates",
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
                "dbo.QueuedEmails",
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
            
            CreateTable(
                "dbo.PermissionRecordUserRoles",
                c => new
                    {
                        PermissionRecord_Id = c.Int(nullable: false),
                        UserRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionRecord_Id, t.UserRole_Id })
                .ForeignKey("dbo.tab_Permission", t => t.PermissionRecord_Id)
                .ForeignKey("dbo.tab_UserRole", t => t.UserRole_Id)
                .Index(t => t.PermissionRecord_Id)
                .Index(t => t.UserRole_Id);
            
            CreateTable(
                "dbo.UserRoleUserTables",
                c => new
                    {
                        UserRole_Id = c.Int(nullable: false),
                        UserTable_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRole_Id, t.UserTable_Id })
                .ForeignKey("dbo.tab_UserRole", t => t.UserRole_Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserTable_Id)
                .Index(t => t.UserRole_Id)
                .Index(t => t.UserTable_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tab_ZhuanZhangTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.UserRoleUserTables", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.UserRoleUserTables", "UserRole_Id", "dbo.tab_UserRole");
            DropForeignKey("dbo.PermissionRecordUserRoles", "UserRole_Id", "dbo.tab_UserRole");
            DropForeignKey("dbo.PermissionRecordUserRoles", "PermissionRecord_Id", "dbo.tab_Permission");
            DropForeignKey("dbo.tab_Permission", "ParentId", "dbo.tab_Permission");
            DropForeignKey("dbo.tab_OAuthTable", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ItemTable", "ZhuanTiId", "dbo.tab_ZhuanTiTable");
            DropForeignKey("dbo.tab_ZhuanTiTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ItemTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ItemTable", "CategoryTypeId", "dbo.tab_CategoryTypeTable");
            DropForeignKey("dbo.tab_ItemTable", "CardId", "dbo.tab_CardTable");
            DropForeignKey("dbo.tab_CategoryTypeTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_CardTable", "UserId", "dbo.tab_UserTable");
            DropIndex("dbo.UserRoleUserTables", new[] { "UserTable_Id" });
            DropIndex("dbo.UserRoleUserTables", new[] { "UserRole_Id" });
            DropIndex("dbo.PermissionRecordUserRoles", new[] { "UserRole_Id" });
            DropIndex("dbo.PermissionRecordUserRoles", new[] { "PermissionRecord_Id" });
            DropIndex("dbo.tab_ZhuanZhangTable", new[] { "UserId" });
            DropIndex("dbo.tab_Permission", new[] { "ParentId" });
            DropIndex("dbo.tab_OAuthTable", new[] { "UserTable_Id" });
            DropIndex("dbo.tab_ZhuanTiTable", new[] { "UserId" });
            DropIndex("dbo.tab_ItemTable", new[] { "UserId" });
            DropIndex("dbo.tab_ItemTable", new[] { "CardId" });
            DropIndex("dbo.tab_ItemTable", new[] { "ZhuanTiId" });
            DropIndex("dbo.tab_ItemTable", new[] { "CategoryTypeId" });
            DropIndex("dbo.tab_CategoryTypeTable", new[] { "UserId" });
            DropIndex("dbo.tab_CardTable", new[] { "UserId" });
            DropTable("dbo.UserRoleUserTables");
            DropTable("dbo.PermissionRecordUserRoles");
            DropTable("dbo.QueuedEmails");
            DropTable("dbo.MessageTemplates");
            DropTable("dbo.Employees");
            DropTable("dbo.tab_ZhuanZhangTable");
            DropTable("dbo.tab_Permission");
            DropTable("dbo.tab_UserRole");
            DropTable("dbo.tab_OAuthTable");
            DropTable("dbo.tab_ZhuanTiTable");
            DropTable("dbo.tab_ItemTable");
            DropTable("dbo.tab_CategoryTypeTable");
            DropTable("dbo.tab_UserTable");
            DropTable("dbo.tab_CardTable");
        }
    }
}
