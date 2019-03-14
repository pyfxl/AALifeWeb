namespace AALife.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class XXX1 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.bse_Parameter");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.bse_Parameter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        SystemName = c.String(nullable: false, maxLength: 20),
                        Value = c.String(nullable: false, maxLength: 20),
                        ParentId = c.Int(nullable: false),
                        IsLeaf = c.Boolean(),
                        IsDefault = c.Boolean(),
                        Rank = c.Byte(),
                        OrderNo = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
