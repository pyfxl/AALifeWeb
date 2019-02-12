namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tab_CardTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_CategoryTypeTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ItemTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_OAuthTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ZhuanTiTable", "UserId", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ZhuanZhangTable", "UserId", "dbo.tab_UserTable");
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        PasswordSalt = c.String(),
                        ResetPassword = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        LastIpAddress = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastLoginDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Active = c.Boolean(nullable: false),
                        IsSystemRole = c.Boolean(nullable: false),
                        SystemName = c.String(),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
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
                "dbo.PermissionRecordCustomerRoles",
                c => new
                    {
                        PermissionRecord_Id = c.Int(nullable: false),
                        CustomerRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionRecord_Id, t.CustomerRole_Id })
                .ForeignKey("dbo.PermissionRecords", t => t.PermissionRecord_Id)
                .ForeignKey("dbo.CustomerRoles", t => t.CustomerRole_Id)
                .Index(t => t.PermissionRecord_Id)
                .Index(t => t.CustomerRole_Id);
            
            AddColumn("dbo.tab_CardTable", "UserTable_Id", c => c.Int());
            AddColumn("dbo.tab_UserTable", "Rank", c => c.Byte());
            AddColumn("dbo.tab_UserTable", "Image", c => c.String(maxLength: 200));
            AddColumn("dbo.tab_UserTable", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.tab_CategoryTypeTable", "UserTable_Id", c => c.Int());
            AddColumn("dbo.tab_ItemTable", "UserTable_Id", c => c.Int());
            AddColumn("dbo.tab_OAuthTable", "UserTable_Id", c => c.Int());
            AddColumn("dbo.tab_ZhuanTiTable", "UserTable_Id", c => c.Int());
            AddColumn("dbo.tab_ZhuanZhangTable", "UserTable_Id", c => c.Int());
            CreateIndex("dbo.tab_CardTable", "UserTable_Id");
            CreateIndex("dbo.tab_CategoryTypeTable", "UserTable_Id");
            CreateIndex("dbo.tab_ItemTable", "UserTable_Id");
            CreateIndex("dbo.tab_OAuthTable", "UserTable_Id");
            CreateIndex("dbo.tab_UserTable", "UserId");
            CreateIndex("dbo.tab_ZhuanTiTable", "UserTable_Id");
            CreateIndex("dbo.tab_ZhuanZhangTable", "UserTable_Id");
            AddForeignKey("dbo.tab_UserTable", "UserId", "dbo.Customers", "Id");
            AddForeignKey("dbo.tab_CardTable", "UserTable_Id", "dbo.tab_UserTable", "Id");
            AddForeignKey("dbo.tab_CategoryTypeTable", "UserTable_Id", "dbo.tab_UserTable", "Id");
            AddForeignKey("dbo.tab_ItemTable", "UserTable_Id", "dbo.tab_UserTable", "Id");
            AddForeignKey("dbo.tab_OAuthTable", "UserTable_Id", "dbo.tab_UserTable", "Id");
            AddForeignKey("dbo.tab_ZhuanTiTable", "UserTable_Id", "dbo.tab_UserTable", "Id");
            AddForeignKey("dbo.tab_ZhuanZhangTable", "UserTable_Id", "dbo.tab_UserTable", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tab_ZhuanZhangTable", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ZhuanTiTable", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_OAuthTable", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_ItemTable", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_CategoryTypeTable", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_CardTable", "UserTable_Id", "dbo.tab_UserTable");
            DropForeignKey("dbo.tab_UserTable", "UserId", "dbo.Customers");
            DropForeignKey("dbo.CustomerRoles", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.PermissionRecordCustomerRoles", "CustomerRole_Id", "dbo.CustomerRoles");
            DropForeignKey("dbo.PermissionRecordCustomerRoles", "PermissionRecord_Id", "dbo.PermissionRecords");
            DropIndex("dbo.PermissionRecordCustomerRoles", new[] { "CustomerRole_Id" });
            DropIndex("dbo.PermissionRecordCustomerRoles", new[] { "PermissionRecord_Id" });
            DropIndex("dbo.tab_ZhuanZhangTable", new[] { "UserTable_Id" });
            DropIndex("dbo.tab_ZhuanTiTable", new[] { "UserTable_Id" });
            DropIndex("dbo.tab_UserTable", new[] { "UserId" });
            DropIndex("dbo.tab_OAuthTable", new[] { "UserTable_Id" });
            DropIndex("dbo.tab_ItemTable", new[] { "UserTable_Id" });
            DropIndex("dbo.tab_CategoryTypeTable", new[] { "UserTable_Id" });
            DropIndex("dbo.CustomerRoles", new[] { "Customer_Id" });
            DropIndex("dbo.tab_CardTable", new[] { "UserTable_Id" });
            DropColumn("dbo.tab_ZhuanZhangTable", "UserTable_Id");
            DropColumn("dbo.tab_ZhuanTiTable", "UserTable_Id");
            DropColumn("dbo.tab_OAuthTable", "UserTable_Id");
            DropColumn("dbo.tab_ItemTable", "UserTable_Id");
            DropColumn("dbo.tab_CategoryTypeTable", "UserTable_Id");
            DropColumn("dbo.tab_UserTable", "UserId");
            DropColumn("dbo.tab_UserTable", "Image");
            DropColumn("dbo.tab_UserTable", "Rank");
            DropColumn("dbo.tab_CardTable", "UserTable_Id");
            DropTable("dbo.PermissionRecordCustomerRoles");
            DropTable("dbo.PermissionRecords");
            DropTable("dbo.CustomerRoles");
            DropTable("dbo.Customers");
            AddForeignKey("dbo.tab_ZhuanZhangTable", "UserId", "dbo.tab_UserTable", "Id");
            AddForeignKey("dbo.tab_ZhuanTiTable", "UserId", "dbo.tab_UserTable", "Id");
            AddForeignKey("dbo.tab_OAuthTable", "UserId", "dbo.tab_UserTable", "Id");
            AddForeignKey("dbo.tab_ItemTable", "UserId", "dbo.tab_UserTable", "Id");
            AddForeignKey("dbo.tab_CategoryTypeTable", "UserId", "dbo.tab_UserTable", "Id");
            AddForeignKey("dbo.tab_CardTable", "UserId", "dbo.tab_UserTable", "Id");
        }
    }
}
