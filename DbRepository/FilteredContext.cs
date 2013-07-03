using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.DbClasses;

namespace DbLayer
{
    public class FilteredContext: IDisposable
    {
        #region Properties and Constructors

        private int _instanceId;
        public int InstanceId { get { return _instanceId; } }
        private readonly MainDbContext _context;

        public FilteredContext(int instanceId)
        {
            _instanceId = instanceId;
            _context = new MainDbContext();
        }

        public void SetCompanyId(int companyId)
        {
            _instanceId = companyId;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion

        #region Tables

        public IQueryable<User> Users
        {
            get { return _context.Users; }
        }

        public IQueryable<Instance> Instances
        {
            get { return _context.Instances; }
        }

        public IQueryable<UserInstance> UserInstances
        {
            get { return _context.UserInstances; }
        }

        public IQueryable<InstanceUsage> InstanceUsages
        {
            get { return _context.InstanceUsages; }
        }

        public IQueryable<TemporaryCode> TemporaryCodes
        {
            get { return _context.TemporaryCodes; }
        }
        #endregion
    }
}
