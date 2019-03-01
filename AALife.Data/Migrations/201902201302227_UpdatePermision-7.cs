namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermision7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_CardTable", "PictureId", c => c.Int());
            AddColumn("dbo.tab_CategoryTypeTable", "PictureId", c => c.Int());
            AddColumn("dbo.tab_ItemTable", "PictureId", c => c.Int());
            AddColumn("dbo.tab_OAuthTable", "PictureId", c => c.Int());
            AddColumn("dbo.tab_ZhuanTiTable", "PictureId", c => c.Int());
            AddColumn("dbo.tab_ZhuanZhangTable", "PictureId", c => c.Int());
            DropColumn("dbo.tab_CardTable", "Image");
            DropColumn("dbo.tab_CategoryTypeTable", "Image");
            DropColumn("dbo.tab_ItemTable", "Image");
            DropColumn("dbo.tab_OAuthTable", "Image");
            DropColumn("dbo.tab_ZhuanTiTable", "Image");
            DropColumn("dbo.tab_ZhuanZhangTable", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_ZhuanZhangTable", "Image", c => c.String(maxLength: 200));
            AddColumn("dbo.tab_ZhuanTiTable", "Image", c => c.String(maxLength: 200));
            AddColumn("dbo.tab_OAuthTable", "Image", c => c.String(maxLength: 200));
            AddColumn("dbo.tab_ItemTable", "Image", c => c.String(maxLength: 200));
            AddColumn("dbo.tab_CategoryTypeTable", "Image", c => c.String(maxLength: 200));
            AddColumn("dbo.tab_CardTable", "Image", c => c.String(maxLength: 200));
            DropColumn("dbo.tab_ZhuanZhangTable", "PictureId");
            DropColumn("dbo.tab_ZhuanTiTable", "PictureId");
            DropColumn("dbo.tab_OAuthTable", "PictureId");
            DropColumn("dbo.tab_ItemTable", "PictureId");
            DropColumn("dbo.tab_CategoryTypeTable", "PictureId");
            DropColumn("dbo.tab_CardTable", "PictureId");
        }
    }
}
