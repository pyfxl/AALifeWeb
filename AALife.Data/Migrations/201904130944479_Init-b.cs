namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initb : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.UserPositionUserDeptments", "UserPosition_Id", "dbo.tab_UserPosition");
            //DropForeignKey("dbo.UserPositionUserDeptments", "UserDeptment_Id", "dbo.tab_UserDeptment");
            //DropIndex("dbo.UserPositionUserDeptments", new[] { "UserPosition_Id" });
            //DropIndex("dbo.UserPositionUserDeptments", new[] { "UserDeptment_Id" });
            //AddColumn("dbo.tab_UserPosition", "DeptmentId", c => c.Guid(nullable: false));
            //CreateIndex("dbo.tab_UserPosition", "DeptmentId");
            //AddForeignKey("dbo.tab_UserPosition", "DeptmentId", "dbo.tab_UserDeptment", "Id");
            //DropTable("dbo.UserPositionUserDeptments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPositionUserDeptments",
                c => new
                    {
                        UserPosition_Id = c.Guid(nullable: false),
                        UserDeptment_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserPosition_Id, t.UserDeptment_Id });
            
            DropForeignKey("dbo.tab_UserPosition", "DeptmentId", "dbo.tab_UserDeptment");
            DropIndex("dbo.tab_UserPosition", new[] { "DeptmentId" });
            DropColumn("dbo.tab_UserPosition", "DeptmentId");
            CreateIndex("dbo.UserPositionUserDeptments", "UserDeptment_Id");
            CreateIndex("dbo.UserPositionUserDeptments", "UserPosition_Id");
            AddForeignKey("dbo.UserPositionUserDeptments", "UserDeptment_Id", "dbo.tab_UserDeptment", "Id");
            AddForeignKey("dbo.UserPositionUserDeptments", "UserPosition_Id", "dbo.tab_UserPosition", "Id");
        }
    }
}
