namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tab_Permission", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_Permission", "Category", c => c.String(maxLength: 20));
        }
    }
}
