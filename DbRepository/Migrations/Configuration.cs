﻿using CommonClasses;
using CommonClasses.DbClasses;

namespace DbLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DbLayer.MainDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DbLayer.MainDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
                context.Components.AddOrUpdate(
                  p => p.ComponentId,
                  new Component { ComponentId = (int)AccessComponent.Home, ComponentName = "Главная страница" },
                  new Component { ComponentId = (int)AccessComponent.Instance, ComponentName = "Компания" },
                  new Component { ComponentId = (int)AccessComponent.Settings, ComponentName = "Настройки" },
                  new Component { ComponentId = (int)AccessComponent.Users, ComponentName = "Пользователи" },
                  new Component { ComponentId = (int)AccessComponent.Roles, ComponentName = "Роли" }
                );
            
        }
    }
}
