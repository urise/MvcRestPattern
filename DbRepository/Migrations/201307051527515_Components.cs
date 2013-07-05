namespace DbLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Components : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Components",
                c => new
                    {
                        ComponentId = c.Int(nullable: false),
                        ComponentName = c.String(nullable: false, maxLength: 128),
                        IsReadOnlyAccess = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ComponentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Components");
        }
    }
}
