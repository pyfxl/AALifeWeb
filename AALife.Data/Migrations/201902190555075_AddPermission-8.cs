namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tab_Permission", "ParentId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tab_Permission", "ParentId", c => c.Int(nullable: false));
        }
    }
}
