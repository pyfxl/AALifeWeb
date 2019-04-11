namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsSystem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.bse_Parameter", "IsSystem", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.bse_Parameter", "IsSystem");
        }
    }
}
