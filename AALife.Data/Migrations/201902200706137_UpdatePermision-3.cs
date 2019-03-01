namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermision3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tab_CardTable", "Rank", c => c.String(maxLength: 10));
            AlterColumn("dbo.tab_CategoryTypeTable", "Rank", c => c.String(maxLength: 10));
            AlterColumn("dbo.tab_ItemTable", "Rank", c => c.String(maxLength: 10));
            AlterColumn("dbo.tab_OAuthTable", "Rank", c => c.String(maxLength: 10));
            AlterColumn("dbo.tab_Permission", "Rank", c => c.String(maxLength: 10));
            AlterColumn("dbo.tab_ZhuanTiTable", "Rank", c => c.String(maxLength: 10));
            AlterColumn("dbo.tab_ZhuanZhangTable", "Rank", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tab_ZhuanZhangTable", "Rank", c => c.Byte());
            AlterColumn("dbo.tab_ZhuanTiTable", "Rank", c => c.Byte());
            AlterColumn("dbo.tab_Permission", "Rank", c => c.Byte());
            AlterColumn("dbo.tab_OAuthTable", "Rank", c => c.Byte());
            AlterColumn("dbo.tab_ItemTable", "Rank", c => c.Byte());
            AlterColumn("dbo.tab_CategoryTypeTable", "Rank", c => c.Byte());
            AlterColumn("dbo.tab_CardTable", "Rank", c => c.Byte());
        }
    }
}
