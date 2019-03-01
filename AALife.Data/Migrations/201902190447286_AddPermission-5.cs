namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_Permission", "Icon", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_Permission", "Icon");
        }
    }
}
