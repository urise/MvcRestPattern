namespace DbLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Login", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Users", "Password", c => c.String(maxLength: 1024));
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 128));
            AlterColumn("dbo.Users", "UserFio", c => c.String(maxLength: 128));
            AlterColumn("dbo.Users", "RegistrationCode", c => c.String(maxLength: 10));
            AlterColumn("dbo.Instances", "InstanceName", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instances", "InstanceName", c => c.String());
            AlterColumn("dbo.Users", "RegistrationCode", c => c.String());
            AlterColumn("dbo.Users", "UserFio", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Login", c => c.String());
        }
    }
}
