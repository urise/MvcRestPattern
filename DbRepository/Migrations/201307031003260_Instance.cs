namespace DbLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Instance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Instances",
                c => new
                    {
                        InstanceId = c.Int(nullable: false, identity: true),
                        InstanceName = c.String(),
                    })
                .PrimaryKey(t => t.InstanceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Instances");
        }
    }
}
