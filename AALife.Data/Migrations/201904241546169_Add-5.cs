namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.usr_UserPermission", "IsPage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.usr_UserPermission", "IsPage");
        }
    }
}
