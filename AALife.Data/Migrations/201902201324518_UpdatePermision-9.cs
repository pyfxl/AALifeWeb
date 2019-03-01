namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePermision9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tab_UserTable", "Remark", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tab_UserTable", "Remark");
        }
    }
}
