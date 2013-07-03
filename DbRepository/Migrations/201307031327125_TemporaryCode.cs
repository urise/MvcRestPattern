namespace DbLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TemporaryCode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TemporaryCodes",
                c => new
                    {
                        TemporaryCodeId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                        ExpireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TemporaryCodeId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TemporaryCodes", new[] { "UserId" });
            DropForeignKey("dbo.TemporaryCodes", "UserId", "dbo.Users");
            DropTable("dbo.TemporaryCodes");
        }
    }
}
