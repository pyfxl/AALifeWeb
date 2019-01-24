namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRemark : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_CardTable", "Remark", c => c.String(maxLength: 100));
            AddColumn("dbo.tab_UserTable", "Remark", c => c.String(maxLength: 100));
            AddColumn("dbo.tab_CategoryTypeTable", "Remark", c => c.String(maxLength: 100));
            AddColumn("dbo.tab_OAuthTable", "Remark", c => c.String(maxLength: 100));
            AddColumn("dbo.tab_ZhuanTiTable", "Remark", c => c.String(maxLength: 100));
            AddColumn("dbo.tab_UserFromTable", "Remark", c => c.String(maxLength: 100));
            AddColumn("dbo.tab_UserLevelTable", "Remark", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_UserLevelTable", "Remark");
            DropColumn("dbo.tab_UserFromTable", "Remark");
            DropColumn("dbo.tab_ZhuanTiTable", "Remark");
            DropColumn("dbo.tab_OAuthTable", "Remark");
            DropColumn("dbo.tab_CategoryTypeTable", "Remark");
            DropColumn("dbo.tab_UserTable", "Remark");
            DropColumn("dbo.tab_CardTable", "Remark");
        }
    }
}
