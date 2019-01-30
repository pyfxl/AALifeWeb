namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPasswordSalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_UserTable", "PasswordSalt", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_UserTable", "PasswordSalt");
        }
    }
}
