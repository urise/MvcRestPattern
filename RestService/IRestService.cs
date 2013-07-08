using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using CommonClasses.DbClasses;
using CommonClasses.InfoClasses;
using CommonClasses.MethodArguments;
using CommonClasses.MethodResults;
using CommonClasses.Models;

namespace RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.NotAllowed)]
    public interface IRestService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "test")]
        void Test();

        #region Login, Passwords & Register

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "logon")]
        LoginResult Logon(LogonArg arg);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "logout/{token}")]
        BaseResult Logout(string token);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "getUserInstances/{token}")]
        MethodResult<IList<Instance>> GetUserInstances(string token);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "logonToInstance/{token}")]
        LoginResult LogonToInstance(string token, int instanceId);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "registerUser")]
        MethodResult<string> RegisterUser(RegisterUser user);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "confirmUserKey/{key}")]
        BaseResult ConfirmUserKey(string key);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "changePassword/{token}")]
        BaseResult ChangePassword(string token, UserPassword userPassword);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "forgotPassword")]
        BaseResult ForgotPassword(UserPassword userPassword);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "createTemporaryCode")]
        MethodResult<PasswordMailInfo> CreateTemporaryCode(string nameOrEmail);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "getUserPasswordByCode/{code}")]
        MethodResult<UserPassword> GetUserPasswordByCode(string code);

        #endregion

        #region Instance
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "createInstance/{token}")]
        MethodResult<int> CreateInstance(string token, string instanceName);
        #endregion
    }
}
