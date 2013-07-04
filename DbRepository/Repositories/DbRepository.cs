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
using CommonClasses.Roles;

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

        private int? _instanceId = null;
        public int? InstanceId
        {
            get { return _instanceId; }
        }
        private bool _releaseContext;
        private AuthInfo _authInfo;

        public int? UserId
        {
            get { return _authInfo == null ? (int?)null : _authInfo.UserId; }
        }

        public void SetInstanceId(int instanceId)
        {
            if (_instanceId != 0)
                throw new Exception("Cannot set instance id for DbRepository if it's already defined");
            _authInfo.InstanceId = instanceId;
            _instanceId = instanceId;
            _context.SetInstanceId(instanceId);
        }

        public void SetAuthInfo(AuthInfo authInfo)
        {
            if (_authInfo != null)
                throw new Exception("Cannot set auth info for DbRepository if it's already defined");
            _authInfo = authInfo;
            _instanceId = authInfo.InstanceId;
            _context.SetInstanceId(authInfo.InstanceId);
        }
        #endregion

        #region Constructors

        public DbRepository(int? instanceId)
        {
            _context = new FilteredContext(instanceId);
            _instanceId = instanceId;
            _releaseContext = true;
        }

        public DbRepository(AuthInfo authInfo)
        {
            _authInfo = authInfo;
            _instanceId = authInfo.InstanceId;
            _context = new FilteredContext(authInfo.InstanceId);
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

        #region Logging

        private void LogToDb(int? userId, string tableName, int recordId, string operation, string details, int? transactionNumber)
        {
            InsertLogToDb(_instanceId, userId, tableName, recordId, operation, details, transactionNumber);
        }

        private void InsertLogToDb(int? instanceId, int? userId, string tableName, int recordId, string operation, string details, int? transactionNumber)
        {
            var dataLog = new DataLog
            {
                InstanceId = instanceId,
                UserId = userId,
                OperationTime = DateTime.Now,
                TableName = tableName,
                RecordId = recordId,
                Operation = operation,
                Details = details,
                TransactionNumber = transactionNumber
            };
            _context.Add(dataLog);
            _context.SaveChanges();
        }

        #endregion

        #region Save

        public int Save<T>(T obj, int? transactionNumber = null) where T : class, IMapping
        {
            if (obj is IConstraintedByInstanceId && (obj as IConstraintedByInstanceId).InstanceId != InstanceId)
            {
                throw new Exception("Save: wrong InstanceId for object type " + obj.GetType().Name + ".");
            }

            int id = obj.PrimaryKeyValue;
            string diffXml = string.Empty;
            if (id == 0)
            {
                _context.Add(obj);
            }
            else
            {
                var record = _context.GetById<T>(id);
                diffXml = XmlHelper.GetDifferenceXml(record, obj);
                ReflectionHelper.CopyAllProperties(obj, record);
            }
            _context.SaveChanges();
            
            if (id == 0)
            {
                LogToDb(UserId, obj.GetType().Name, obj.PrimaryKeyValue, "I", XmlHelper.GetObjectXml(obj), transactionNumber);
            }
            else
            {
                LogToDb(UserId, "CurrencyClasses", obj.PrimaryKeyValue, "U", diffXml, transactionNumber);
            }
            return obj.PrimaryKeyValue;
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
            var lastRecord = Context.InstanceUsages.Where(ucu => ucu.UserId == userId).OrderBy(ucu => ucu.LoginDate).AsEnumerable().LastOrDefault();
            return lastRecord == null ? 0 : lastRecord.InstanceId;
        }

        public bool CheckIfUserLinkedToInstance(int userId, int instanceId)
        {
            return Context.UserInstances.Any(x => x.UserId == userId && x.InstanceId == instanceId);
        }

        public bool LoginIsNotUnique(string login)
        {
            return Context.Users.Any(u => u.Login.ToLower() == login.ToLower());
        }

        public bool EmailIsNotUnique(string email, int userId = 0)
        {
            return Context.Users.Any(u => u.Email == email && u.UserId != userId);
        }

        public User GetUserByKey(string key)
        {
            return Context.Users.FirstOrDefault(u => u.RegistrationCode == key);
        }

        public User GetUserByEmail(string email)
        {
            return Context.Users.FirstOrDefault(u => u.Email == email);
        }

        public string GetUserNameByCode(string code)
        {
            return Context.TemporaryCodes.Where(x => x.Code == code).Select(x => x.User.Login).FirstOrDefault();
        }

        public TemporaryCode GetTemporaryCodeByUserId(int userId)
        {
            return Context.TemporaryCodes.FirstOrDefault(x => x.UserId == userId);
        }

        public IList<Instance> GetUserInstances(int userId)
        {
            return (from instance in Context.Instances
                       join userInstances in Context.UserInstances.Where(u=>u.UserId == userId) 
                        on instance.InstanceId equals userInstances.InstanceId
                            select instance).AsEnumerable().OrderBy(i=>i.InstanceName).ToList();
        }

        public Instance GetInstanceById(int id)
        {
            return Context.Instances.FirstOrDefault(i=>i.InstanceId == id);
        }

        //TODO:
        public int SaveInstanceUsage(InstanceUsage instanceUsage)
        { return 0; }
        public int SaveTemporaryCode(TemporaryCode temporaryCode)
        { return 0; }


        public UserAccess GetUserAccess(int userId)
        { return null; }

        public void DeleteTemporaryCode(int temporaryCodeId) { }
        #endregion
    }
}
