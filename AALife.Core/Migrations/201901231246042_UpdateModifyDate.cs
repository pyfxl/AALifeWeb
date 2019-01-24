namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModifyDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tab_CardTable", "ModifyDate", c => c.DateTime());
            AlterColumn("dbo.tab_UserTable", "ModifyDate", c => c.DateTime());
            AlterColumn("dbo.tab_CategoryTypeTable", "ModifyDate", c => c.DateTime());
            AlterColumn("dbo.tab_ItemTable", "ModifyDate", c => c.DateTime());
            AlterColumn("dbo.tab_OAuthTable", "ModifyDate", c => c.DateTime());
            AlterColumn("dbo.tab_ZhuanTiTable", "ModifyDate", c => c.DateTime());
            AlterColumn("dbo.tab_UserFromTable", "ModifyDate", c => c.DateTime());
            AlterColumn("dbo.tab_UserLevelTable", "ModifyDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tab_UserLevelTable", "ModifyDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tab_UserFromTable", "ModifyDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tab_ZhuanTiTable", "ModifyDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tab_OAuthTable", "ModifyDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tab_ItemTable", "ModifyDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tab_CategoryTypeTable", "ModifyDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tab_UserTable", "ModifyDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tab_CardTable", "ModifyDate", c => c.DateTime(nullable: false));
        }
    }
}
