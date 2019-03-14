namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Childa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.bse_Parameter", "ParentId", c => c.Int());
            CreateIndex("dbo.bse_Parameter", "ParentId");
            AddForeignKey("dbo.bse_Parameter", "ParentId", "dbo.bse_Parameter", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.bse_Parameter", "ParentId", "dbo.bse_Parameter");
            DropIndex("dbo.bse_Parameter", new[] { "ParentId" });
            AlterColumn("dbo.bse_Parameter", "ParentId", c => c.Int(nullable: false));
        }
    }
}
