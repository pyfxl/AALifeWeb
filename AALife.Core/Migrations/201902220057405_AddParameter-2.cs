namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParameter2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.bse_Parameter", "IsLeaf", c => c.Boolean());
            AddColumn("dbo.bse_Parameter", "IsDefault", c => c.Boolean());
            DropColumn("dbo.bse_Parameter", "ModifyDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.bse_Parameter", "ModifyDate", c => c.DateTime());
            DropColumn("dbo.bse_Parameter", "IsDefault");
            DropColumn("dbo.bse_Parameter", "IsLeaf");
        }
    }
}
