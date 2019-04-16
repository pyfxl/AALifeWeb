namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inita : DbMigration
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
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tab_UserTable",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 20),
                        UserPassword = c.String(nullable: false, maxLength: 50),
                        PasswordSalt = c.String(maxLength: 10),
                        UserNickName = c.String(maxLength: 50),
                        UserCode = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(maxLength: 20),
                        UserPhone = c.String(maxLength: 50),
                        UserEmail = c.String(maxLength: 200),
                        UserImage = c.String(maxLength: 200),
                        UserTheme = c.String(maxLength: 10),
                        UserLevel = c.Byte(nullable: false),
                        UserFrom = c.String(nullable: false, maxLength: 10),
                        CreateDate = c.DateTime(nullable: false),
                        Remark = c.String(maxLength: 100),
                        Synchronize = c.Byte(nullable: false),
                        ModifyDate = c.DateTime(),
                        IsAdmin = c.Boolean(nullable: false),
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
                        UserId = c.Guid(nullable: false),
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
                        UserId = c.Guid(nullable: false),
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
                        UserId = c.Guid(nullable: false),
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
                        UserId = c.Guid(nullable: false),
                        OldUserId = c.Guid(nullable: false),
                        OAuthBound = c.Int(nullable: false),
                        OAuthFrom = c.String(maxLength: 10),
                        UserTable_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserTable_Id)
                .Index(t => t.UserTable_Id);
            
            CreateTable(
                "dbo.tab_UserDeptment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 20),
                        OrgType = c.String(maxLength: 10),
                        OrgLevel = c.Int(),
                        Notes = c.String(maxLength: 200),
                        Category = c.String(maxLength: 20),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserDeptment", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.tab_UserPosition",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 20),
                        Notes = c.String(maxLength: 200),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserPosition", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.tab_UserPermission",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AreaName = c.String(maxLength: 20),
                        ControllerName = c.String(maxLength: 20),
                        ActionName = c.String(maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 20),
                        IconName = c.String(maxLength: 50),
                        IsButton = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                        Rank = c.Byte(),
                        OrderNo = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserPermission", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.tab_UserRole",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        SystemName = c.String(nullable: false, maxLength: 20),
                        Notes = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserPositionUserDeptments",
                c => new
                    {
                        UserPosition_Id = c.Guid(nullable: false),
                        UserDeptment_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserPosition_Id, t.UserDeptment_Id })
                .ForeignKey("dbo.tab_UserPosition", t => t.UserPosition_Id)
                .ForeignKey("dbo.tab_UserDeptment", t => t.UserDeptment_Id)
                .Index(t => t.UserPosition_Id)
                .Index(t => t.UserDeptment_Id);
            
            CreateTable(
                "dbo.UserPermissionUserDeptments",
                c => new
                    {
                        UserPermission_Id = c.Guid(nullable: false),
                        UserDeptment_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserPermission_Id, t.UserDeptment_Id })
                .ForeignKey("dbo.tab_UserPermission", t => t.UserPermission_Id)
                .ForeignKey("dbo.tab_UserDeptment", t => t.UserDeptment_Id)
                .Index(t => t.UserPermission_Id)
                .Index(t => t.UserDeptment_Id);
            
            CreateTable(
                "dbo.UserPermissionUserPositions",
                c => new
                    {
                        UserPermission_Id = c.Guid(nullable: false),
                        UserPosition_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserPermission_Id, t.UserPosition_Id })
                .ForeignKey("dbo.tab_UserPermission", t => t.UserPermission_Id)
                .ForeignKey("dbo.tab_UserPosition", t => t.UserPosition_Id)
                .Index(t => t.UserPermission_Id)
                .Index(t => t.UserPosition_Id);
            
            CreateTable(
                "dbo.UserRoleUserPermissions",
                c => new
                    {
                        UserRole_Id = c.Guid(nullable: false),
                        UserPermission_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRole_Id, t.UserPermission_Id })
                .ForeignKey("dbo.tab_UserRole", t => t.UserRole_Id)
                .ForeignKey("dbo.tab_UserPermission", t => t.UserPermission_Id)
                .Index(t => t.UserRole_Id)
                .Index(t => t.UserPermission_Id);
            
            CreateTable(
                "dbo.UserRoleUserTables",
                c => new
                    {
                        UserRole_Id = c.Guid(nullable: false),
                        UserTable_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRole_Id, t.UserTable_Id })
                .ForeignKey("dbo.tab_UserRole", t => t.UserRole_Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserTable_Id)
                .Index(t => t.UserRole_Id)
                .Index(t => t.UserTable_Id);
            
            CreateTable(
                "dbo.UserPositionUserTables",
                c => new
                    {
                        UserPosition_Id = c.Guid(nullable: false),
                        UserTable_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserPosition_Id, t.UserTable_Id })
                .ForeignKey("dbo.tab_UserPosition", t => t.UserPosition_Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserTable_Id)
                .Index(t => t.UserPosition_Id)
                .Index(t => t.UserTable_Id);
            
            CreateTable(
                "dbo.UserDeptmentUserTables",
                c => new
                    {
                        UserDeptment_Id = c.Guid(nullable: false),
                        UserTable_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserDeptment_Id, t.UserTable_Id })
                .ForeignKey("dbo.tab_UserDeptment", t => t.UserDeptment_Id)
                .ForeignKey("dbo.tab_UserTable", t => t.UserTable_Id)
                .Index(t => t.UserDeptment_Id)
                .Index(t => t.UserTable_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tab_ZhuanZhangTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.UserDeptmentUserTables", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.UserDeptmentUserTables", "UserDeptment_Id", "dbo.tab_UserDeptment");
            DropForeignKey("dbo.UserPositionUserTables", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.UserPositionUserTables", "UserPosition_Id", "dbo.tab_UserPosition");
            DropForeignKey("dbo.UserRoleUserTables", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.UserRoleUserTables", "UserRole_Id", "dbo.tab_UserRole");
            DropForeignKey("dbo.UserRoleUserPermissions", "UserPermission_Id", "dbo.tab_UserPermission");
            DropForeignKey("dbo.UserRoleUserPermissions", "UserRole_Id", "dbo.tab_UserRole");
            DropForeignKey("dbo.UserPermissionUserPositions", "UserPosition_Id", "dbo.tab_UserPosition");
            DropForeignKey("dbo.UserPermissionUserPositions", "UserPermission_Id", "dbo.tab_UserPermission");
            DropForeignKey("dbo.UserPermissionUserDeptments", "UserDeptment_Id", "dbo.tab_UserDeptment");
            DropForeignKey("dbo.UserPermissionUserDeptments", "UserPermission_Id", "dbo.tab_UserPermission");
            DropForeignKey("dbo.tab_UserPermission", "ParentId", "dbo.tab_UserPermission");
            DropForeignKey("dbo.UserPositionUserDeptments", "UserDeptment_Id", "dbo.tab_UserDeptment");
            DropForeignKey("dbo.UserPositionUserDeptments", "UserPosition_Id", "dbo.tab_UserPosition");
            DropForeignKey("dbo.tab_UserPosition", "ParentId", "dbo.tab_UserPosition");
            DropForeignKey("dbo.tab_UserDeptment", "ParentId", "dbo.tab_UserDeptment");
            DropForeignKey("dbo.tab_OAuthTable", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ItemTable", "ZhuanTiId", "dbo.tab_ZhuanTiTable");
            DropForeignKey("dbo.tab_ZhuanTiTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ItemTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ItemTable", "CategoryTypeId", "dbo.tab_CategoryTypeTable");
            DropForeignKey("dbo.tab_ItemTable", "CardId", "dbo.tab_CardTable");
            DropForeignKey("dbo.tab_CategoryTypeTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_CardTable", "UserId", "dbo.tab_UserTable");
            DropIndex("dbo.UserDeptmentUserTables", new[] { "UserTable_Id" });
            DropIndex("dbo.UserDeptmentUserTables", new[] { "UserDeptment_Id" });
            DropIndex("dbo.UserPositionUserTables", new[] { "UserTable_Id" });
            DropIndex("dbo.UserPositionUserTables", new[] { "UserPosition_Id" });
            DropIndex("dbo.UserRoleUserTables", new[] { "UserTable_Id" });
            DropIndex("dbo.UserRoleUserTables", new[] { "UserRole_Id" });
            DropIndex("dbo.UserRoleUserPermissions", new[] { "UserPermission_Id" });
            DropIndex("dbo.UserRoleUserPermissions", new[] { "UserRole_Id" });
            DropIndex("dbo.UserPermissionUserPositions", new[] { "UserPosition_Id" });
            DropIndex("dbo.UserPermissionUserPositions", new[] { "UserPermission_Id" });
            DropIndex("dbo.UserPermissionUserDeptments", new[] { "UserDeptment_Id" });
            DropIndex("dbo.UserPermissionUserDeptments", new[] { "UserPermission_Id" });
            DropIndex("dbo.UserPositionUserDeptments", new[] { "UserDeptment_Id" });
            DropIndex("dbo.UserPositionUserDeptments", new[] { "UserPosition_Id" });
            DropIndex("dbo.tab_ZhuanZhangTable", new[] { "UserId" });
            DropIndex("dbo.tab_UserPermission", new[] { "ParentId" });
            DropIndex("dbo.tab_UserPosition", new[] { "ParentId" });
            DropIndex("dbo.tab_UserDeptment", new[] { "ParentId" });
            DropIndex("dbo.tab_OAuthTable", new[] { "UserTable_Id" });
            DropIndex("dbo.tab_ZhuanTiTable", new[] { "UserId" });
            DropIndex("dbo.tab_ItemTable", new[] { "UserId" });
            DropIndex("dbo.tab_ItemTable", new[] { "CardId" });
            DropIndex("dbo.tab_ItemTable", new[] { "ZhuanTiId" });
            DropIndex("dbo.tab_ItemTable", new[] { "CategoryTypeId" });
            DropIndex("dbo.tab_CategoryTypeTable", new[] { "UserId" });
            DropIndex("dbo.tab_CardTable", new[] { "UserId" });
            DropTable("dbo.UserDeptmentUserTables");
            DropTable("dbo.UserPositionUserTables");
            DropTable("dbo.UserRoleUserTables");
            DropTable("dbo.UserRoleUserPermissions");
            DropTable("dbo.UserPermissionUserPositions");
            DropTable("dbo.UserPermissionUserDeptments");
            DropTable("dbo.UserPositionUserDeptments");
            DropTable("dbo.tab_ZhuanZhangTable");
            DropTable("dbo.tab_UserRole");
            DropTable("dbo.tab_UserPermission");
            DropTable("dbo.tab_UserPosition");
            DropTable("dbo.tab_UserDeptment");
            DropTable("dbo.tab_OAuthTable");
            DropTable("dbo.tab_ZhuanTiTable");
            DropTable("dbo.tab_ItemTable");
            DropTable("dbo.tab_CategoryTypeTable");
            DropTable("dbo.tab_UserTable");
            DropTable("dbo.tab_CardTable");
        }
    }
}
