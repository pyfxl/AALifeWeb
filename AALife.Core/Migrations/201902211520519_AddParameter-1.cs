namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParameter1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.bse_Parameter", "ModifyDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.bse_Parameter", "ModifyDate");
        }
    }
}
