namespace DbLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataLogs",
                c => new
                    {
                        DataLogId = c.Int(nullable: false, identity: true),
                        InstanceId = c.Int(),
                        UserId = c.Int(),
                        OperationTime = c.DateTime(nullable: false),
                        TableName = c.String(maxLength: 128),
                        RecordId = c.Int(nullable: false),
                        Operation = c.String(maxLength: 1),
                        Details = c.String(storeType: "xml"),
                        TransactionNumber = c.Int(),
                    })
                .PrimaryKey(t => t.DataLogId)
                .ForeignKey("dbo.Instances", t => t.InstanceId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.InstanceId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DataLogs", new[] { "UserId" });
            DropIndex("dbo.DataLogs", new[] { "InstanceId" });
            DropForeignKey("dbo.DataLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.DataLogs", "InstanceId", "dbo.Instances");
            DropTable("dbo.DataLogs");
        }
    }
}
