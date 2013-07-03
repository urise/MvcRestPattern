namespace DbLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserInstance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInstances",
                c => new
                    {
                        UserInstanceId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        InstanceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserInstanceId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Instances", t => t.InstanceId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.InstanceId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserInstances", new[] { "InstanceId" });
            DropIndex("dbo.UserInstances", new[] { "UserId" });
            DropForeignKey("dbo.UserInstances", "InstanceId", "dbo.Instances");
            DropForeignKey("dbo.UserInstances", "UserId", "dbo.Users");
            DropTable("dbo.UserInstances");
        }
    }
}
