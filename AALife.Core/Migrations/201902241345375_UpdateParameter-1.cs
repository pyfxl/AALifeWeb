namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateParameter1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.bse_Parameter", "ParentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.bse_Parameter", "ParentId", c => c.Int());
        }
    }
}
