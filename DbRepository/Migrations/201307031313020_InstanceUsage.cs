namespace DbLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstanceUsage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InstanceUsages",
                c => new
                    {
                        InstanceUsageId = c.Int(nullable: false, identity: true),
                        InstanceId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        LoginDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InstanceUsageId)
                .ForeignKey("dbo.Instances", t => t.InstanceId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.InstanceId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.InstanceUsages", new[] { "UserId" });
            DropIndex("dbo.InstanceUsages", new[] { "InstanceId" });
            DropForeignKey("dbo.InstanceUsages", "UserId", "dbo.Users");
            DropForeignKey("dbo.InstanceUsages", "InstanceId", "dbo.Instances");
            DropTable("dbo.InstanceUsages");
        }
    }
}
