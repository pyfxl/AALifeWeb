namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPositionUserDeptments", "UserPosition_Id", "dbo.tab_UserPosition");
            DropForeignKey("dbo.UserPositionUserDeptments", "UserDeptment_Id", "dbo.tab_UserDeptment");
            DropIndex("dbo.UserPositionUserDeptments", new[] { "UserPosition_Id" });
            DropIndex("dbo.UserPositionUserDeptments", new[] { "UserDeptment_Id" });
            AddColumn("dbo.tab_UserPosition", "DeptmentId", c => c.Guid(nullable: true));
            CreateIndex("dbo.tab_UserPosition", "DeptmentId");
            AddForeignKey("dbo.tab_UserPosition", "DeptmentId", "dbo.tab_UserDeptment", "Id");
            //DropTable("dbo.UserPositionUserDeptments");
        }

        public override void Down()
        {
        }
    }
}
