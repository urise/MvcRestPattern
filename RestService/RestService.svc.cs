using System;
using System.Collections.Generic;
using System.Diagnostics;
using BusinessLayer.Authentication;
using CommonClasses;
using CommonClasses.DbClasses;
using CommonClasses.Helpers;
using CommonClasses.InfoClasses;
using CommonClasses.MethodArguments;
using CommonClasses.MethodResults;
using CommonClasses.Models;
using CommonClasses.Roles;
using DbLayer.Repositories;
using Interfaces.Enums;
using NLog;
using BusinessLayer.Managers;

namespace RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RestService.svc or RestService.svc.cs at the Solution Explorer and start debugging.
    public class RestService : IRestService
    {
        #region Auxilliary Methods

        private Logger _logger = LogManager.GetLogger("AasRest");

        private string GetPreviousMethodName()
        {
            return GetPreviousMethod().Name;
        }

        private System.Reflection.MethodBase GetPreviousMethod()
        {
            var stackFrames = new StackTrace().GetFrames();
            if (stackFrames != null) return stackFrames[2].GetMethod();
            return null;
        }

        private ResultTypeEnum CheckAccess(string token, out AuthInfo authInfo)
        {
            authInfo = AuthTokens.Instance.GetAuth(token);
            if (authInfo == null)
            {
                return ResultTypeEnum.NotLoggedIn;
            }
            var stackFrames = new StackTrace().GetFrames();
            if (stackFrames != null)
            {
                var callingMethod = stackFrames[2].GetMethod();
                if (!authInfo.AccessGranted(callingMethod))
                {
                    return ResultTypeEnum.AccessDenied;
                }
            }
            return ResultTypeEnum.Success;
        }

        private T RunLoginManagerMethod<T>(Func<LoginManager, T> func) where T : BaseResult, new()
        {
            using (var db = new DbRepository((int?)null))
            {
                try
                {
                    var manager = new LoginManager(db);
                    var result = func(manager);
                    if (result.IsError())
                        _logger.Warn(GetPreviousMethodName() + ": " + result.ErrorMessage);
                    else
                        _logger.Info(GetPreviousMethodName());
                    return result;
                }
                catch (Exception ex)
                {
                    var message = ex.SmartMessage();
                    _logger.Fatal(GetPreviousMethodName() + ": " + message);
                    return new T { ErrorMessage = message };
                }
            }
        }

        private T RunManagerMethod<TClass, T>(string token, Func<TClass, T> func)
            where T : BaseResult, new()
            where TClass : IDbManager, new()
        {
            AuthInfo authInfo;
            var checkResult = CheckAccess(token, out authInfo);
            if (checkResult != ResultTypeEnum.Success)
                return new T { ResultType = checkResult };

            using (var db = new DbRepository(authInfo))
            {
                try
                {
                    var instance = new TClass { Db = db };
                    T result = func(instance);
                    if (result.IsError())
                        _logger.Warn(GetPreviousMethodName() + ": " + result.ErrorMessage);
                    else
                        _logger.Info(GetPreviousMethodName());
                    return result;
                }
                catch (Exception ex)
                {
                    var message = ex.SmartMessage();
                    _logger.Fatal(GetPreviousMethodName() + ": " + message);
                    return new T { ErrorMessage = message };
                }
            }
        }

        private void ClearAuthCompanyId(string token)
        {
            var authInfo = AuthTokens.Instance.GetAuth(token);
            if (authInfo != null)
            {
                authInfo.InstanceId = 0;
            }
        }
        #endregion

        #region Login, Passwords & Register

        public LoginResult Logon(LogonArg arg)
        {
            return RunLoginManagerMethod(lm => lm.Logon(arg));
        }

        public void Test()
        {
            RunLoginManagerMethod(lm => lm.Test());
        }

        public LoginResult LogonToInstance(string token, int instanceId)
        {
            return RunLoginManagerMethod(lm => lm.LogonToInstance(token, instanceId));
        }
        
        public MethodResult<IList<Instance>> GetUserInstances(string token)
        {
            return RunManagerMethod<LoginManager, MethodResult<IList<Instance>>>(token, lm => lm.GetUserInstances());
        }

        public BaseResult Logout(string token)
        {
            return RunLoginManagerMethod(lm => lm.Logout(token));
        }

        public MethodResult<string> RegisterUser(RegisterUser user)
        {
            return RunLoginManagerMethod(lm => lm.RegisterUser(user));
        }

        public BaseResult ConfirmUserKey(string key)
        {
            return RunLoginManagerMethod(lm => lm.ConfirmUserKey(key));
        }

        public BaseResult ChangePassword(string token, UserPassword userPassword)
        {
            return RunManagerMethod<LoginManager, BaseResult>(token, lm => lm.ChangePassword(userPassword));
        }

        public BaseResult ForgotPassword(UserPassword userPassword)
        {
            return RunLoginManagerMethod(lm => lm.ForgotPassword(userPassword));
        }

        public MethodResult<PasswordMailInfo> CreateTemporaryCode(string nameOrEmail)
        {
            return RunLoginManagerMethod(lm => lm.CreateTemporaryCode(nameOrEmail));
        }

        public MethodResult<UserPassword> GetUserPasswordByCode(string code)
        {
            return RunLoginManagerMethod(lm => lm.GetUserPasswordByCode(code));
        }

        #endregion

        #region Instance

        public MethodResult<int> CreateInstance(string token, string instanceName)
        {
            ClearAuthCompanyId(token);
            return RunManagerMethod<InstanceManager, MethodResult<int>>(token, cm => cm.CreateInstance(instanceName));
        }
        #endregion

        #region User
        [AccessTier(AccessComponent.Users, AccessLevel.Read)]
        public MethodResult<List<UserInstanceInfo>> GetUserInstanceList(string token)
        {
            return RunManagerMethod<UserManager, MethodResult<List<UserInstanceInfo>>>(token, rep => rep.GetUserInstanceList());
        }
        #endregion
    }
}
