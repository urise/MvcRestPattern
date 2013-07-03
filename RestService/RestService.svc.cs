using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CommonClasses.Helpers;
using CommonClasses.MethodArguments;
using CommonClasses.MethodResults;
using DbLayer.Repositories;
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

        private T RunLoginManagerMethod<T>(Func<LoginManager, T> func) where T : BaseResult, new()
        {
            using (var db = new DbRepository(0))
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

        #endregion

        public LoginResult Logon(LogonArg arg)
        {
            return RunLoginManagerMethod(lm => lm.Logon(arg));
        }
    }
}
