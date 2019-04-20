namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.bse_Log", "FullMessage", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.bse_Log", "FullMessage", c => c.String(maxLength: 1000));
        }
    }
}
