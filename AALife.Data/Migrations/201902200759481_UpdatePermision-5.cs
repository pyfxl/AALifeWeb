namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermision5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_Permission", "OrderNo", c => c.String(maxLength: 10));
            DropColumn("dbo.tab_Permission", "Order");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tab_Permission", "Order", c => c.String(maxLength: 10));
            DropColumn("dbo.tab_Permission", "OrderNo");
        }
    }
}
