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

        #endregion
    }
}
