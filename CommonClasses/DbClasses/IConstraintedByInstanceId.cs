using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClasses.DbClasses
{
    public interface IConstraintedByInstanceId
    {
        int InstanceId { get; }
    }
}
