using System.Collections.Generic;
using CommonClasses.InfoClasses;
using CommonClasses.MethodArguments;
using CommonClasses.MethodResults;
using CommonClasses.Models;
using Interfaces.Enums;
using CommonClasses.DbClasses;

namespace ServiceProxy
{
    public partial class ServiceProxySingleton
    {
        #region Login

        public LoginResult Logon(LogonArg arg)
        {
            return SendPostRequest<LoginResult, LogonArg>("logon", arg, false);
        }

        public LoginResult LogonToCompany(int instanceId)
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
    }
}
