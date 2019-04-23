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
                        PictureId = c.Int(),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.usr_UserTable", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.usr_UserTable",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 20),
                        UserPassword = c.String(nullable: false, maxLength: 50),
                        PasswordSalt = c.String(maxLength: 10),
                        UserCode = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(maxLength: 50),
                        UserPhone = c.String(maxLength: 50),
                        UserEmail = c.String(maxLength: 200),
                        UserImage = c.String(maxLength: 200),
                        UserTheme = c.String(maxLength: 10),
                        UserLevel = c.Byte(nullable: false),
                        UserFrom = c.String(nullable: false, maxLength: 10),
                        CreateDate = c.DateTime(nullable: false),
                        Remark = c.String(maxLength: 200),
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
                .ForeignKey("dbo.usr_UserTable", t => t.UserId)
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
                .ForeignKey("dbo.usr_UserTable", t => t.UserId)
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
                .ForeignKey("dbo.usr_UserTable", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.usr_UserDeptment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 20),
                        OrgType = c.String(maxLength: 10),
                        OrgLevel = c.Int(),
                        Notes = c.String(maxLength: 200),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.usr_UserDeptment", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.usr_UserPosition",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Code = c.String(nullable: false, maxLength: 20),
                        Notes = c.String(maxLength: 200),
                        DeptmentId = c.Guid(nullable: false),
                        TitleId = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.usr_UserPosition", t => t.ParentId)
                .ForeignKey("dbo.usr_UserDeptment", t => t.DeptmentId)
                .ForeignKey("dbo.usr_UserTitle", t => t.TitleId)
                .Index(t => t.DeptmentId)
                .Index(t => t.TitleId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.usr_UserTitle",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Code = c.String(nullable: false, maxLength: 20),
                        Notes = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.usr_UserPermission",
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
                .ForeignKey("dbo.usr_UserPermission", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.usr_UserRole",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        SystemName = c.String(nullable: false, maxLength: 20),
                        Notes = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.usr_UserTablePositions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MainPosition = c.Boolean(),
                        Position_Id = c.Guid(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.usr_UserPosition", t => t.Position_Id)
                .ForeignKey("dbo.usr_UserTable", t => t.User_Id)
                .Index(t => t.Position_Id)
                .Index(t => t.User_Id);
            
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
                .ForeignKey("dbo.usr_UserTable", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoleUserPermissions",
                c => new
                    {
                        UserRole_Id = c.Guid(nullable: false),
                        UserPermission_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRole_Id, t.UserPermission_Id })
                .ForeignKey("dbo.usr_UserRole", t => t.UserRole_Id)
                .ForeignKey("dbo.usr_UserPermission", t => t.UserPermission_Id)
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
                .ForeignKey("dbo.usr_UserRole", t => t.UserRole_Id)
                .ForeignKey("dbo.usr_UserTable", t => t.UserTable_Id)
                .Index(t => t.UserRole_Id)
                .Index(t => t.UserTable_Id);
            
            CreateTable(
                "dbo.UserPermissionUserTitles",
                c => new
                    {
                        UserPermission_Id = c.Guid(nullable: false),
                        UserTitle_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserPermission_Id, t.UserTitle_Id })
                .ForeignKey("dbo.usr_UserPermission", t => t.UserPermission_Id)
                .ForeignKey("dbo.usr_UserTitle", t => t.UserTitle_Id)
                .Index(t => t.UserPermission_Id)
                .Index(t => t.UserTitle_Id);
            
            CreateTable(
                "dbo.UserDeptmentUserTables",
                c => new
                    {
                        UserDeptment_Id = c.Guid(nullable: false),
                        UserTable_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserDeptment_Id, t.UserTable_Id })
                .ForeignKey("dbo.usr_UserDeptment", t => t.UserDeptment_Id)
                .ForeignKey("dbo.usr_UserTable", t => t.UserTable_Id)
                .Index(t => t.UserDeptment_Id)
                .Index(t => t.UserTable_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tab_ZhuanZhangTable", "UserId", "dbo.usr_UserTable");
            DropForeignKey("dbo.UserDeptmentUserTables", "UserTable_Id", "dbo.usr_UserTable");
            DropForeignKey("dbo.UserDeptmentUserTables", "UserDeptment_Id", "dbo.usr_UserDeptment");
            DropForeignKey("dbo.usr_UserTablePositions", "User_Id", "dbo.usr_UserTable");
            DropForeignKey("dbo.usr_UserTablePositions", "Position_Id", "dbo.usr_UserPosition");
            DropForeignKey("dbo.usr_UserPosition", "TitleId", "dbo.usr_UserTitle");
            DropForeignKey("dbo.UserPermissionUserTitles", "UserTitle_Id", "dbo.usr_UserTitle");
            DropForeignKey("dbo.UserPermissionUserTitles", "UserPermission_Id", "dbo.usr_UserPermission");
            DropForeignKey("dbo.UserRoleUserTables", "UserTable_Id", "dbo.usr_UserTable");
            DropForeignKey("dbo.UserRoleUserTables", "UserRole_Id", "dbo.usr_UserRole");
            DropForeignKey("dbo.UserRoleUserPermissions", "UserPermission_Id", "dbo.usr_UserPermission");
            DropForeignKey("dbo.UserRoleUserPermissions", "UserRole_Id", "dbo.usr_UserRole");
            DropForeignKey("dbo.usr_UserPermission", "ParentId", "dbo.usr_UserPermission");
            DropForeignKey("dbo.usr_UserPosition", "DeptmentId", "dbo.usr_UserDeptment");
            DropForeignKey("dbo.usr_UserPosition", "ParentId", "dbo.usr_UserPosition");
            DropForeignKey("dbo.usr_UserDeptment", "ParentId", "dbo.usr_UserDeptment");
            DropForeignKey("dbo.tab_ItemTable", "ZhuanTiId", "dbo.tab_ZhuanTiTable");
            DropForeignKey("dbo.tab_ZhuanTiTable", "UserId", "dbo.usr_UserTable");
            DropForeignKey("dbo.tab_ItemTable", "UserId", "dbo.usr_UserTable");
            DropForeignKey("dbo.tab_ItemTable", "CategoryTypeId", "dbo.tab_CategoryTypeTable");
            DropForeignKey("dbo.tab_ItemTable", "CardId", "dbo.tab_CardTable");
            DropForeignKey("dbo.tab_CategoryTypeTable", "UserId", "dbo.usr_UserTable");
            DropForeignKey("dbo.tab_CardTable", "UserId", "dbo.usr_UserTable");
            DropIndex("dbo.UserDeptmentUserTables", new[] { "UserTable_Id" });
            DropIndex("dbo.UserDeptmentUserTables", new[] { "UserDeptment_Id" });
            DropIndex("dbo.UserPermissionUserTitles", new[] { "UserTitle_Id" });
            DropIndex("dbo.UserPermissionUserTitles", new[] { "UserPermission_Id" });
            DropIndex("dbo.UserRoleUserTables", new[] { "UserTable_Id" });
            DropIndex("dbo.UserRoleUserTables", new[] { "UserRole_Id" });
            DropIndex("dbo.UserRoleUserPermissions", new[] { "UserPermission_Id" });
            DropIndex("dbo.UserRoleUserPermissions", new[] { "UserRole_Id" });
            DropIndex("dbo.tab_ZhuanZhangTable", new[] { "UserId" });
            DropIndex("dbo.usr_UserTablePositions", new[] { "User_Id" });
            DropIndex("dbo.usr_UserTablePositions", new[] { "Position_Id" });
            DropIndex("dbo.usr_UserPermission", new[] { "ParentId" });
            DropIndex("dbo.usr_UserPosition", new[] { "ParentId" });
            DropIndex("dbo.usr_UserPosition", new[] { "TitleId" });
            DropIndex("dbo.usr_UserPosition", new[] { "DeptmentId" });
            DropIndex("dbo.usr_UserDeptment", new[] { "ParentId" });
            DropIndex("dbo.tab_ZhuanTiTable", new[] { "UserId" });
            DropIndex("dbo.tab_ItemTable", new[] { "UserId" });
            DropIndex("dbo.tab_ItemTable", new[] { "CardId" });
            DropIndex("dbo.tab_ItemTable", new[] { "ZhuanTiId" });
            DropIndex("dbo.tab_ItemTable", new[] { "CategoryTypeId" });
            DropIndex("dbo.tab_CategoryTypeTable", new[] { "UserId" });
            DropIndex("dbo.tab_CardTable", new[] { "UserId" });
            DropTable("dbo.UserDeptmentUserTables");
            DropTable("dbo.UserPermissionUserTitles");
            DropTable("dbo.UserRoleUserTables");
            DropTable("dbo.UserRoleUserPermissions");
            DropTable("dbo.tab_ZhuanZhangTable");
            DropTable("dbo.usr_UserTablePositions");
            DropTable("dbo.usr_UserRole");
            DropTable("dbo.usr_UserPermission");
            DropTable("dbo.usr_UserTitle");
            DropTable("dbo.usr_UserPosition");
            DropTable("dbo.usr_UserDeptment");
            DropTable("dbo.tab_ZhuanTiTable");
            DropTable("dbo.tab_ItemTable");
            DropTable("dbo.tab_CategoryTypeTable");
            DropTable("dbo.usr_UserTable");
            DropTable("dbo.tab_CardTable");
        }
    }
}
