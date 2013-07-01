using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.DbClasses;

namespace DbRepository
{
    public class MainDbContext: DbContext
    {
        public MainDbContext(): base("Name=MainDbContext") {}

        public DbSet<User> Users { get; set; }
    }
}
