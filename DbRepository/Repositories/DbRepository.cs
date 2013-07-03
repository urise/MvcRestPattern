using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.DbClasses;
using CommonClasses.DbRepositoryInterface;
using CommonClasses.Helpers;
using CommonClasses.InfoClasses;
using CommonClasses.MethodArguments;

namespace DbLayer.Repositories
{
    public partial class DbRepository: IDbRepository
    {
        #region Properties and Variables

        private FilteredContext _context;
        private FilteredContext Context
        {
            get { return _context; }
        }

        private int _instanceId;
        public int InstanceId
        {
            get { return _instanceId; }
        }
        private bool _releaseContext;
        private AuthInfo _authInfo;

        public int UserId
        {
            get { return _authInfo == null ? 0 : _authInfo.UserId; }
        }

        #endregion

        #region Constructors

        public DbRepository(int instanceId)
        {
            _context = new FilteredContext(instanceId);
            _instanceId = instanceId;
            _releaseContext = true;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_releaseContext)
                _context.Dispose();
        }

        #endregion

        #region Login

        public User GetUserByLogin(string login)
        {
            return Context.Users.FirstOrDefault(x => x.Login.ToLower() == login.ToLower());
        }

        public User GetAndAuthenticateUser(LogonArg arg)
        {
            var user = GetUserByLogin(arg.Login);
            if (user != null)
            {
                var hash = CryptHelper.GetSha512Base64Hash(arg.Salt + user.Password);
                if (hash.Equals(arg.PasswordHash))
                    return user;
            }
            return null;
        }

        public int GetLastUsedInstanceId(int userId)
        {
            return 0;
            //var lastRecord = Context.UserCompanyUsages.Where(ucu => ucu.UserId == userId).OrderBy(ucu => ucu.Date).AsEnumerable().LastOrDefault();
            //return lastRecord == null ? 0 : lastRecord.UsedCompanyId;
        }

        #endregion
    }
}
