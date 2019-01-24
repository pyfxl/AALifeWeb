namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategoryTypeId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tab_ItemTable", "CategoryTypeId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tab_ItemTable", "CategoryTypeId", c => c.Int(nullable: false));
        }
    }
}
