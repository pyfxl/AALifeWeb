namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePasswordLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tab_UserTable", "UserPassword", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tab_UserTable", "UserPassword", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
