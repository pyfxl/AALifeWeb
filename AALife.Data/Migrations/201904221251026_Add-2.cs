namespace AALife.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.usr_UserTablePositions", newName: "usr_UsersPositions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.usr_UsersPositions", newName: "usr_UserTablePositions");
        }
    }
}
