namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermision2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_Permission", "Remark", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_Permission", "Remark");
        }
    }
}
