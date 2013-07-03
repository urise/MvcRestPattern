using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.DbClasses;

namespace DbLayer
{
    public class MainDbContext: DbContext
    {
        public MainDbContext(): base("Name=MainDbContext") {}

        public DbSet<User> Users { get; set; }
        public DbSet<Instance> Instances { get; set; }
        public DbSet<UserInstance> UserInstances { get; set; }
    }
}
