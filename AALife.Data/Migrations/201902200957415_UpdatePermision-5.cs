namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermision5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tab_CardTable", "OrderNo");
            DropColumn("dbo.tab_CategoryTypeTable", "OrderNo");
            DropColumn("dbo.tab_ItemTable", "OrderNo");
            DropColumn("dbo.tab_OAuthTable", "OrderNo");
            DropColumn("dbo.tab_ZhuanTiTable", "OrderNo");
            DropColumn("dbo.tab_ZhuanZhangTable", "OrderNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_ZhuanZhangTable", "OrderNo", c => c.String(maxLength: 10));
            AddColumn("dbo.tab_ZhuanTiTable", "OrderNo", c => c.String(maxLength: 10));
            AddColumn("dbo.tab_OAuthTable", "OrderNo", c => c.String(maxLength: 10));
            AddColumn("dbo.tab_ItemTable", "OrderNo", c => c.String(maxLength: 10));
            AddColumn("dbo.tab_CategoryTypeTable", "OrderNo", c => c.String(maxLength: 10));
            AddColumn("dbo.tab_CardTable", "OrderNo", c => c.String(maxLength: 10));
        }
    }
}
