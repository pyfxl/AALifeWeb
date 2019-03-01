namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tab_Permission", "Icon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_Permission", "Icon", c => c.String(maxLength: 20));
        }
    }
}
