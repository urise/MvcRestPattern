using System.Collections.Generic;
using CommonClasses.InfoClasses;
using CommonClasses.MethodArguments;
using CommonClasses.MethodResults;
using CommonClasses.Models;
using Interfaces.Enums;

namespace ServiceProxy
{
    public partial class ServiceProxySingleton
    {
        #region Login

        public LoginResult Logon(LogonArg arg)
        {
            Delay();
            return SendPostRequest<LoginResult, LogonArg>("logon", arg, false);
        }

        //public LoginResult LogonToCompany(CompanyArg arg)
        //{
        //    Delay();
        //    return SendPostRequest<LoginResult, CompanyArg>("logonToCompany", arg);
        //}

        //public BaseResult Logout()
        //{
        //    Delay();
        //    return SendGetRequest<BaseResult>("logout");
        //}

        #endregion
    }
}
