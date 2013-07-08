namespace DbLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Role_UserRole_ComponentRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 128),
                        RoleType = c.Int(nullable: false),
                        InstanceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.Instances", t => t.InstanceId, cascadeDelete: true)
                .Index(t => t.InstanceId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        InstanceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Instances", t => t.InstanceId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.InstanceId);
            
            CreateTable(
                "dbo.ComponentRoles",
                c => new
                    {
                        ComponentRoleId = c.Int(nullable: false, identity: true),
                        ComponentId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        AccessLevel = c.Int(nullable: false),
                        InstanceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ComponentRoleId)
                .ForeignKey("dbo.Components", t => t.ComponentId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Instances", t => t.InstanceId, cascadeDelete: true)
                .Index(t => t.ComponentId)
                .Index(t => t.RoleId)
                .Index(t => t.InstanceId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ComponentRoles", new[] { "InstanceId" });
            DropIndex("dbo.ComponentRoles", new[] { "RoleId" });
            DropIndex("dbo.ComponentRoles", new[] { "ComponentId" });
            DropIndex("dbo.UserRoles", new[] { "InstanceId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", new[] { "InstanceId" });
            DropForeignKey("dbo.ComponentRoles", "InstanceId", "dbo.Instances");
            DropForeignKey("dbo.ComponentRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ComponentRoles", "ComponentId", "dbo.Components");
            DropForeignKey("dbo.UserRoles", "InstanceId", "dbo.Instances");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Roles", "InstanceId", "dbo.Instances");
            DropTable("dbo.ComponentRoles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
        }
    }
}
