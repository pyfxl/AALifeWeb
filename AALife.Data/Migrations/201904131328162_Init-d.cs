namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initd : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tab_UserPosition", new[] { "DeptmentId" });
            AlterColumn("dbo.tab_UserPosition", "DeptmentId", c => c.Guid());
            CreateIndex("dbo.tab_UserPosition", "DeptmentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tab_UserPosition", new[] { "DeptmentId" });
            AlterColumn("dbo.tab_UserPosition", "DeptmentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.tab_UserPosition", "DeptmentId");
        }
    }
}
