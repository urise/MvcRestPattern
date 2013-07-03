using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Authentication;
using CommonClasses;
using CommonClasses.DbClasses;
using CommonClasses.DbRepositoryInterface;
using CommonClasses.Helpers;
using CommonClasses.InfoClasses;
using CommonClasses.MethodArguments;
using CommonClasses.MethodResults;
using Interfaces.Enums;

namespace BusinessLayer.Managers
{
    public class LoginManager: CommonManager
    {
        #region Constructors

        public LoginManager() { }

        public LoginManager(IDbRepository repository) : base(repository) { }

        #endregion

        private User GetUserByLoginAndPassword(LogonArg arg)
        {
            switch (AppConfiguration.AuthenticationMethod)
            {
                case AuthenticationType.Native:
                    return Db.GetAndAuthenticateUser(arg);
                case AuthenticationType.ActiveDirectory:
                    var adHelper = new AdHelper(AppConfiguration.LDAPServer);
                    BaseResult adResult = adHelper.Authenticate(arg.Login, arg.PasswordHash);
                    if (adResult.IsError()) return null;
                    return Db.GetUserByLogin(arg.Login);
            }
            return null;
        }

        public LoginResult Logon(LogonArg arg)
        {
            var user = GetUserByLoginAndPassword(arg);
            if (user == null)
                return new LoginResult { ErrorMessage = Messages.WrongLoginOrPassword };
            if (!user.IsActive)
                return new LoginResult { ErrorMessage = Messages.UserEmailUnapproved };

            var authInfo = new AuthInfo { UserId = user.UserId };

            var token = AuthTokens.Instance.AddAuth(authInfo);

            var companyArg = GetDefaultCompanyArg(arg);
            return companyArg != null
                        ? LogonToCompany(token, companyArg)
                        : LogonUserWithoutCompany(token, user.UserId);
        }

        private LoginResult LogonUserWithoutCompany(string token, int userId)
        {
            return new LoginResult { Token = token, LastUsedCompanyId = Db.GetLastUsedCompanyId(userId) };
        }

        private CompanyArg GetDefaultCompanyArg(LogonArg arg)
        {
            if (arg.DefaultCompanyId != 0)
            {
                return new CompanyArg(arg.DefaultCompanyId, arg.Salt, arg.FinanceKey);
            }
            return null;
        }

        public LoginResult LogonToCompany(string token, CompanyArg arg)
        {
            return null;

            //var authInfo = AuthTokens.Instance.GetAuth(token);
            //if (authInfo == null)
            //{
            //    return new LoginResult { ResultType = ResultTypeEnum.NotLoggedIn };
            //}

            //authInfo.CompanyId = arg.CompanyId;
            //Db.SetAuthInfo(authInfo);
            //authInfo.UserAccess = Db.GetUserAccess(authInfo.UserId);

            //var cmanager = new CompanyManager(Db);
            //var checkResult = cmanager.TryLoginToCompany(arg);

            //if (checkResult.IsError())
            //{
            //    authInfo.CompanyId = 0;
            //    if (!checkResult.CompanyIsLinked)
            //        return LogonUserWithoutCompany(token, authInfo.UserId);
            //    return new LoginResult { ErrorMessage = checkResult.ErrorMessage };
            //}

            //authInfo.FinanceKeyIsEntered = checkResult.FinanceKeyIsEntered;
            //authInfo.FinanceKeyIsNeeded = checkResult.FinanceKeyIsNeeded;

            //authInfo.SetFinanceKey(arg.FinanceKey);
            //bool financeKeyIsEntered = !authInfo.FinanceKeyIsNeeded || authInfo.FinanceKeyIsEntered;
            //return new LoginResult
            //{
            //    Token = token,
            //    CompanyId = arg.CompanyId,
            //    CompanyName = checkResult.CompanyName,
            //    FinanceKeyIsEntered = financeKeyIsEntered,
            //    CompanyHasKey = authInfo.FinanceKeyIsNeeded,
            //    Access = authInfo.UserAccess
            //};
        }
    }
}
