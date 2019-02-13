namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Data2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PermissionRecordUserRoles", "PermissionRecord_Id", "dbo.PermissionRecords");
            DropForeignKey("dbo.PermissionRecordUserRoles", "UserRole_Id", "dbo.UserRoles");
            DropForeignKey("dbo.UserRoles", "UserTable_Id", "dbo.UserTables");
            DropForeignKey("dbo.tab_CardTable", "UserId", "dbo.UserTables");
            DropForeignKey("dbo.tab_CategoryTypeTable", "UserId", "dbo.UserTables");
            DropForeignKey("dbo.tab_ItemTable", "UserId", "dbo.UserTables");
            DropForeignKey("dbo.tab_OAuthTable", "UserId", "dbo.UserTables");
            DropForeignKey("dbo.tab_ZhuanTiTable", "UserId", "dbo.UserTables");
            DropForeignKey("dbo.tab_ZhuanZhangTable", "UserId", "dbo.UserTables");
            DropIndex("dbo.tab_CardTable", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "UserTable_Id" });
            DropIndex("dbo.tab_CategoryTypeTable", new[] { "UserId" });
            DropIndex("dbo.tab_ItemTable", new[] { "UserId" });
            DropIndex("dbo.tab_OAuthTable", new[] { "UserId" });
            DropIndex("dbo.tab_ZhuanTiTable", new[] { "UserId" });
            DropIndex("dbo.tab_ZhuanZhangTable", new[] { "UserId" });
            DropIndex("dbo.PermissionRecordUserRoles", new[] { "PermissionRecord_Id" });
            DropIndex("dbo.PermissionRecordUserRoles", new[] { "UserRole_Id" });
            //DropTable("dbo.UserTables");
            //DropTable("dbo.UserRoles");
            //DropTable("dbo.PermissionRecords");
            //DropTable("dbo.PermissionRecordUserRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PermissionRecordUserRoles",
                c => new
                    {
                        PermissionRecord_Id = c.Int(nullable: false),
                        UserRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionRecord_Id, t.UserRole_Id });
            
            CreateTable(
                "dbo.PermissionRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SystemName = c.String(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsSystemRole = c.Boolean(nullable: false),
                        SystemName = c.String(),
                        UserTable_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserPassword = c.String(),
                        PasswordSalt = c.String(),
                        UserNickName = c.String(),
                        UserEmail = c.String(),
                        UserImage = c.String(),
                        UserTheme = c.String(),
                        UserLevel = c.Byte(nullable: false),
                        UserFrom = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(),
                        Synchronize = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.PermissionRecordUserRoles", "UserRole_Id");
            CreateIndex("dbo.PermissionRecordUserRoles", "PermissionRecord_Id");
            CreateIndex("dbo.tab_ZhuanZhangTable", "UserId");
            CreateIndex("dbo.tab_ZhuanTiTable", "UserId");
            CreateIndex("dbo.tab_OAuthTable", "UserId");
            CreateIndex("dbo.tab_ItemTable", "UserId");
            CreateIndex("dbo.tab_CategoryTypeTable", "UserId");
            CreateIndex("dbo.UserRoles", "UserTable_Id");
            CreateIndex("dbo.tab_CardTable", "UserId");
            AddForeignKey("dbo.tab_ZhuanZhangTable", "UserId", "dbo.UserTables", "Id");
            AddForeignKey("dbo.tab_ZhuanTiTable", "UserId", "dbo.UserTables", "Id");
            AddForeignKey("dbo.tab_OAuthTable", "UserId", "dbo.UserTables", "Id");
            AddForeignKey("dbo.tab_ItemTable", "UserId", "dbo.UserTables", "Id");
            AddForeignKey("dbo.tab_CategoryTypeTable", "UserId", "dbo.UserTables", "Id");
            AddForeignKey("dbo.tab_CardTable", "UserId", "dbo.UserTables", "Id");
            AddForeignKey("dbo.UserRoles", "UserTable_Id", "dbo.UserTables", "Id");
            AddForeignKey("dbo.PermissionRecordUserRoles", "UserRole_Id", "dbo.UserRoles", "Id");
            AddForeignKey("dbo.PermissionRecordUserRoles", "PermissionRecord_Id", "dbo.PermissionRecords", "Id");
        }
    }
}
