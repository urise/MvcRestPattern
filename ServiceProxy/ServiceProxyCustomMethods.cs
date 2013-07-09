using System.Collections.Generic;
using CommonClasses.InfoClasses;
using CommonClasses.MethodArguments;
using CommonClasses.MethodResults;
using CommonClasses.Models;
using CommonClasses.DbClasses;

namespace ServiceProxy
{
    public partial class ServiceProxySingleton
    {
        #region ChangePassword

        public BaseResult ChangePassword(UserPassword userPassword)
        {
            Delay();
            return SendPostRequest<BaseResult, UserPassword>("changePassword", userPassword);
        }

        public BaseResult ForgotPassword(UserPassword userPassword)
        {
            Delay();
            return SendPostRequest<BaseResult, UserPassword>("forgotPassword", userPassword, false);
        }

        public MethodResult<PasswordMailInfo> CreateTemporaryCode(string nameOrEmail)
        {
            Delay();
            return SendPostRequest<MethodResult<PasswordMailInfo>, string>("createTemporaryCode", nameOrEmail, false);
        }

        public MethodResult<UserPassword> GetUserPasswordByCode(string code)
        {
            Delay();
            return SendGetRequest<MethodResult<UserPassword>>("getUserPasswordByCode", code, false);
        }
        #endregion

        #region Instance
        public MethodResult<int> CreateInstance(string instanceName)
        {
            Delay();
            return SendPostRequest<MethodResult<int>, string>("createInstance", instanceName);
        }
        #endregion

        #region Login

        public LoginResult Logon(LogonArg arg)
        {
            return SendPostRequest<LoginResult, LogonArg>("logon", arg, false);
        }

        public LoginResult LogonToInstance(int instanceId)
        {
            Delay();
            return SendPostRequest<LoginResult, int>("logonToInstance", instanceId);
        }

        public BaseResult Logout()
        {
            Delay();
            return SendGetRequest<BaseResult>("logout");
        }

        public MethodResult<IList<Instance>> GetUserInstances()
        {
            Delay();
            return SendGetRequest<MethodResult<IList<Instance>>>("getUserInstances");
        }

        #endregion

        #region Register

        public MethodResult<string> RegisterUser(RegisterUser user)
        {
            Delay();
            return SendPostRequest<MethodResult<string>, RegisterUser>("registerUser", user, false);
        }

        public BaseResult ConfirmUserKey(string key)
        {
            Delay();
            return SendGetRequest<BaseResult>("confirmUserKey", key, false);
        }

        #endregion

        #region Users Methods
        public MethodResult<List<UserInstanceInfo>> GetUserInstanceList()
        {
            Delay();
            return SendGetRequest<MethodResult<List<UserInstanceInfo>>>("getUserInstanceList");
        }
        #endregion
    }
}
