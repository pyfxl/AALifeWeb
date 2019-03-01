namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermision4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_CardTable", "Order", c => c.String(maxLength: 10));
            AddColumn("dbo.tab_CategoryTypeTable", "Order", c => c.String(maxLength: 10));
            AddColumn("dbo.tab_ItemTable", "Order", c => c.String(maxLength: 10));
            AddColumn("dbo.tab_OAuthTable", "Order", c => c.String(maxLength: 10));
            AddColumn("dbo.tab_Permission", "Order", c => c.String(maxLength: 10));
            AddColumn("dbo.tab_ZhuanTiTable", "Order", c => c.String(maxLength: 10));
            AddColumn("dbo.tab_ZhuanZhangTable", "Order", c => c.String(maxLength: 10));
            AlterColumn("dbo.tab_CardTable", "Rank", c => c.Byte());
            AlterColumn("dbo.tab_CategoryTypeTable", "Rank", c => c.Byte());
            AlterColumn("dbo.tab_ItemTable", "Rank", c => c.Byte());
            AlterColumn("dbo.tab_OAuthTable", "Rank", c => c.Byte());
            AlterColumn("dbo.tab_Permission", "Rank", c => c.Byte());
            AlterColumn("dbo.tab_ZhuanTiTable", "Rank", c => c.Byte());
            AlterColumn("dbo.tab_ZhuanZhangTable", "Rank", c => c.Byte());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tab_ZhuanZhangTable", "Rank", c => c.String(maxLength: 10));
            AlterColumn("dbo.tab_ZhuanTiTable", "Rank", c => c.String(maxLength: 10));
            AlterColumn("dbo.tab_Permission", "Rank", c => c.String(maxLength: 10));
            AlterColumn("dbo.tab_OAuthTable", "Rank", c => c.String(maxLength: 10));
            AlterColumn("dbo.tab_ItemTable", "Rank", c => c.String(maxLength: 10));
            AlterColumn("dbo.tab_CategoryTypeTable", "Rank", c => c.String(maxLength: 10));
            AlterColumn("dbo.tab_CardTable", "Rank", c => c.String(maxLength: 10));
            DropColumn("dbo.tab_ZhuanZhangTable", "Order");
            DropColumn("dbo.tab_ZhuanTiTable", "Order");
            DropColumn("dbo.tab_Permission", "Order");
            DropColumn("dbo.tab_OAuthTable", "Order");
            DropColumn("dbo.tab_ItemTable", "Order");
            DropColumn("dbo.tab_CategoryTypeTable", "Order");
            DropColumn("dbo.tab_CardTable", "Order");
        }
    }
}
