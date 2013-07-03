using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClasses.DbClasses
{
    public class InstanceUsage
    {
        public int InstanceUsageId { get; set; }
        public int InstanceId { get; set; }
        public int UserId { get; set; }
        public DateTime LoginDate { get; set; }

        public virtual Instance Instance { get; set; }
        public virtual User User { get; set; }

    }
}
