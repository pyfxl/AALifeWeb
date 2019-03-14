namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDownload : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.bse_Parameter", "OrderNo", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.bse_Parameter", "OrderNo", c => c.String(maxLength: 10));
        }
    }
}
