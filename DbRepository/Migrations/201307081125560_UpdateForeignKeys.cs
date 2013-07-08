namespace DbLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserInstances", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserInstances", "InstanceId", "dbo.Instances");
            DropForeignKey("dbo.InstanceUsages", "InstanceId", "dbo.Instances");
            DropForeignKey("dbo.InstanceUsages", "UserId", "dbo.Users");
            DropForeignKey("dbo.TemporaryCodes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Roles", "InstanceId", "dbo.Instances");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ComponentRoles", "ComponentId", "dbo.Components");
            DropForeignKey("dbo.ComponentRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.UserInstances", new[] { "UserId" });
            DropIndex("dbo.UserInstances", new[] { "InstanceId" });
            DropIndex("dbo.InstanceUsages", new[] { "InstanceId" });
            DropIndex("dbo.InstanceUsages", new[] { "UserId" });
            DropIndex("dbo.TemporaryCodes", new[] { "UserId" });
            DropIndex("dbo.Roles", new[] { "InstanceId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.ComponentRoles", new[] { "ComponentId" });
            DropIndex("dbo.ComponentRoles", new[] { "RoleId" });
            AddForeignKey("dbo.UserInstances", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.UserInstances", "InstanceId", "dbo.Instances", "InstanceId");
            AddForeignKey("dbo.InstanceUsages", "InstanceId", "dbo.Instances", "InstanceId");
            AddForeignKey("dbo.InstanceUsages", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.TemporaryCodes", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Roles", "InstanceId", "dbo.Instances", "InstanceId");
            AddForeignKey("dbo.UserRoles", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "RoleId");
            AddForeignKey("dbo.ComponentRoles", "ComponentId", "dbo.Components", "ComponentId");
            AddForeignKey("dbo.ComponentRoles", "RoleId", "dbo.Roles", "RoleId");
            CreateIndex("dbo.UserInstances", "UserId");
            CreateIndex("dbo.UserInstances", "InstanceId");
            CreateIndex("dbo.InstanceUsages", "InstanceId");
            CreateIndex("dbo.InstanceUsages", "UserId");
            CreateIndex("dbo.TemporaryCodes", "UserId");
            CreateIndex("dbo.Roles", "InstanceId");
            CreateIndex("dbo.UserRoles", "UserId");
            CreateIndex("dbo.UserRoles", "RoleId");
            CreateIndex("dbo.ComponentRoles", "ComponentId");
            CreateIndex("dbo.ComponentRoles", "RoleId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ComponentRoles", new[] { "RoleId" });
            DropIndex("dbo.ComponentRoles", new[] { "ComponentId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", new[] { "InstanceId" });
            DropIndex("dbo.TemporaryCodes", new[] { "UserId" });
            DropIndex("dbo.InstanceUsages", new[] { "UserId" });
            DropIndex("dbo.InstanceUsages", new[] { "InstanceId" });
            DropIndex("dbo.UserInstances", new[] { "InstanceId" });
            DropIndex("dbo.UserInstances", new[] { "UserId" });
            DropForeignKey("dbo.ComponentRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ComponentRoles", "ComponentId", "dbo.Components");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Roles", "InstanceId", "dbo.Instances");
            DropForeignKey("dbo.TemporaryCodes", "UserId", "dbo.Users");
            DropForeignKey("dbo.InstanceUsages", "UserId", "dbo.Users");
            DropForeignKey("dbo.InstanceUsages", "InstanceId", "dbo.Instances");
            DropForeignKey("dbo.UserInstances", "InstanceId", "dbo.Instances");
            DropForeignKey("dbo.UserInstances", "UserId", "dbo.Users");
            CreateIndex("dbo.ComponentRoles", "RoleId");
            CreateIndex("dbo.ComponentRoles", "ComponentId");
            CreateIndex("dbo.UserRoles", "RoleId");
            CreateIndex("dbo.UserRoles", "UserId");
            CreateIndex("dbo.Roles", "InstanceId");
            CreateIndex("dbo.TemporaryCodes", "UserId");
            CreateIndex("dbo.InstanceUsages", "UserId");
            CreateIndex("dbo.InstanceUsages", "InstanceId");
            CreateIndex("dbo.UserInstances", "InstanceId");
            CreateIndex("dbo.UserInstances", "UserId");
            AddForeignKey("dbo.ComponentRoles", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
            AddForeignKey("dbo.ComponentRoles", "ComponentId", "dbo.Components", "ComponentId", cascadeDelete: true);
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
            AddForeignKey("dbo.UserRoles", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Roles", "InstanceId", "dbo.Instances", "InstanceId", cascadeDelete: true);
            AddForeignKey("dbo.TemporaryCodes", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.InstanceUsages", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.InstanceUsages", "InstanceId", "dbo.Instances", "InstanceId", cascadeDelete: true);
            AddForeignKey("dbo.UserInstances", "InstanceId", "dbo.Instances", "InstanceId", cascadeDelete: true);
            AddForeignKey("dbo.UserInstances", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
