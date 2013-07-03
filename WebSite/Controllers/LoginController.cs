using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CommonClasses;
//using CommonClasses.Helpers;
//using CommonClasses.MethodArguments;
//using CommonClasses.MethodResults;
//using CommonClasses.Models;
//using ServiceProxy;
using CommonClasses.Helpers;
using CommonClasses.MethodArguments;
using CommonClasses.MethodResults;
using CommonClasses.Models;
using ServiceProxy;
using WebSite.Helpers;

namespace WebSite.Controllers
{
    public class LoginController : Controller
    {
        //#region LogOn

        public ActionResult LogOn()
        {
            if (SessionHelper.IsAthorized())
            {
                if (SessionHelper.IsInstanceSelected())
                {
                    TempData["FinanceKey"] = null;
                    var returnUrl = TempData["ReturnUrl"] != null ? TempData["ReturnUrl"].ToString() : "";
                    if (IsValidUrlToRedirect(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("PayRolls", "PayRolls");
                }
                if (SessionHelper.LastUsedInstanceId != null)
                    TempData["LastInstance"] = SessionHelper.LastUsedInstanceId;
                SelectUserInstances();
                return View();
            }


            var message = Session[Constants.SESSION_FORCED_LOGOUT] as string;
            if (!string.IsNullOrEmpty(message)) TempData["LoginErrors"] = message;
            return View();
        }

        private void SelectUserInstances()
        {
            var result = ServiceProxySingleton.Instance.GetUserInstances();
            if (result.IsNotLoggedIn()) SessionHelper.ClearSession();
            if (result.IsSuccess())
                ViewBag.UserCompanies = result.AttachedObject;
            else TempData["LoginErrors"] = result.ErrorMessage;
        }

        private bool IsValidUrlToRedirect(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\");
        }

        [HttpPost]
        public ActionResult LogOn(UserModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string salt, passwordHash;
                switch (AppConfiguration.AuthenticationMethod)
                {
                    case AuthenticationType.Native:
                        salt = RandomHelper.GetRandomString(10);
                        passwordHash = CryptHelper.GetSha512Base64Hash(salt + CryptHelper.GetSha512Base64Hash(model.Login.ToLower() + model.Password));
                        break;
                    default:
                        salt = string.Empty;
                        passwordHash = model.Password;
                        break;
                }

                LoginResult loginResult = ServiceProxySingleton.Instance.Logon(new LogonArg(model.Login.ToLower(), passwordHash, salt, model.FinanceKey));

                if (loginResult.IsSuccess())
                {
                    Session.RemoveAll();
                    Session[Constants.SESSION_INSTANCE_ID] = loginResult.InstanceId;
                    SessionHelper.CompanyName = GetViewCompanyName(loginResult.InstanceName);
                    Session[Constants.SESSION_AUTH_INFO] = loginResult.Token;
                    SessionHelper.UserName = model.Login;
                    SessionHelper.LastUsedInstanceId = loginResult.LastUsedInstanceId;
                    Session[Constants.SESSION_FORCED_LOGOUT] = null;
                    SessionHelper.Permissions = loginResult.Access;
                    TempData["FinanceKey"] = model.FinanceKey;
                    TempData["ReturnUrl"] = returnUrl;
                    return RedirectToAction("LogOn", "Login");
                }
                ModelState.AddModelError("", loginResult.ErrorMessage);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //[HttpPost]
        //public ActionResult LogonToCompany(int companyId, string finKey)
        //{
        //    var salt = RandomHelper.GetRandomString(10);
        //    var loginResult = ServiceProxySingleton.Instance.LogonToCompany(new CompanyArg(companyId, salt, finKey));
        //    if (loginResult.IsNotLoggedIn()) return SessionHelper.ClearSession();

        //    if (loginResult.IsError()) return Json(loginResult);

        //    Session[Constants.SESSION_INSTANCE_ID] = loginResult.CompanyId;
        //    Session[Constants.SESSION_VIEW_INSTANCE_NAME] = GetViewCompanyName(loginResult.CompanyName);
        //    SessionHelper.IsFinanceKeyEntered = loginResult.FinanceKeyIsEntered;
        //    SessionHelper.CompanyHasKey = loginResult.CompanyHasKey;
        //    Session[Constants.SESSION_LAST_LOGGED_COMPANY] = loginResult.CompanyId;
        //    SessionHelper.Permissions = loginResult.Access;

        //    return Json(new {});
        //}

        //public ActionResult SelectCompany()
        //{
        //    SessionHelper.ClearCompanyInfoFromSession();
        //    return RedirectToAction("LogOn", "Login");
        //}

        //[SessionAuthorize]
        //[HttpPost]
        //public ActionResult AddCompany(NewCompanyArg arg)
        //{
        //    var result = ServiceProxySingleton.Instance.CreateCompany(arg);
        //    if (result.IsNotLoggedIn()) return SessionHelper.ClearSession();

        //    if (result.IsError()) return Json(result);

        //    return Json(new { NewId = result.AttachedObject });
        //}

        //#endregion

        //#region LogOff

        //public ActionResult LogOff()
        //{
        //    var logoutResult = ServiceProxySingleton.Instance.Logout();
        //    if (logoutResult != null)
        //    {
        //        if (logoutResult.IsSuccess())
        //        {
        //            Session.Clear();
        //        }
        //        else
        //            ModelState.AddModelError("", logoutResult.ErrorMessage);
        //    }
            
        //    return RedirectToAction("LogOn", "Login");
        //}

        //#endregion

        //#region Register

        //public ActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(RegisterUser user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        user.Password = CryptHelper.GetSha512Base64Hash(user.Login.ToLower() + user.Password);
        //        var result = ServiceProxySingleton.Instance.RegisterUser(user);
        //        if (result.IsError())
        //            ModelState.AddModelError("", result.ErrorMessage);
        //        else
        //        {
        //            user.RegistrationCode = result.AttachedObject;
        //            var helper = new EmailController("ConfirmEmail", user);
        //            helper.SendConfirmEmail();
        //            ViewBag.Succes = true;
        //        }
        //    }
        //    else
        //    {
        //        var errors =
        //            ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).Distinct().ToArray();
        //        foreach (var e in errors)
        //            ModelState.AddModelError("", Service.ErrorMsgToRussian(e));
        //    }
        //    return View(user);
        //}

        //public ActionResult RegisterConfirm(string key)
        //{
        //    if (!string.IsNullOrEmpty(key))
        //    {
        //        var result = ServiceProxySingleton.Instance.ConfirmUserKey(key);
        //        if (result.IsSuccess())
        //            TempData["SuccessMessage"] = Service.ConfirmMailSuccess;
        //        else
        //            TempData["LoginErrors"] = Service.ConfirmMailError;
        //    }
        //    return RedirectToAction("LogOn", "Login");
        //}

        //#endregion

        //#region ChangePassword
        
        //public ActionResult ChangePassword(string key)
        //{
        //    if (key == null)
        //    {
        //        var login = Session[Constants.SESSION_USER_NAME];
        //        if (login == null) return View();
        //        var userPassword = new UserPassword(login.ToString(), true);
        //        Session["ReturnUrl"] = Request.UrlReferrer;
        //        return View(userPassword);
        //    }
        //    else
        //    {
        //        var result = ServiceProxySingleton.Instance.GetUserPasswordByCode(key);
        //        if (result.IsError())
        //        {
        //            SessionHelper.ClearSession(result.ErrorMessage);
        //            return RedirectToAction("LogOn", "Login");
        //        }
        //        return View(result.AttachedObject);
        //    }
        //}

        //[HttpPost]
        //public ActionResult ChangePassword(UserPassword userPassword)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        userPassword.Password = CryptHelper.GetSha512Base64Hash(userPassword.UserName.ToLower() + userPassword.Password);

        //        BaseResult result;

        //        if (userPassword.OldPasswordNeeded)
        //        {
        //            userPassword.Salt = RandomHelper.GetRandomString(10);
        //            userPassword.OldPassword = CryptHelper.GetSha512Base64Hash(userPassword.Salt
        //                + CryptHelper.GetSha512Base64Hash(userPassword.UserName.ToLower() + userPassword.OldPassword));
        //            result = ServiceProxySingleton.Instance.ChangePassword(userPassword);
        //        }
        //        else
        //            result = ServiceProxySingleton.Instance.ForgotPassword(userPassword);
        //        if (result.IsSuccess())
        //        {
        //            if (Session["ReturnUrl"] != null)
        //            {
        //                var url = Session["ReturnUrl"].ToString();
        //                Session["ReturnUrl"] = null;
        //                return Redirect(url);
        //            }
        //            TempData["SuccessMessage"] = Service.ChangePasswordSuccess;
        //            return RedirectToAction("LogOn", "Login");
        //        }
        //        ModelState.AddModelError("", result.ErrorMessage);
                
        //    }
        //    else
        //    {
        //        var errors =
        //            ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).Distinct().ToArray();
        //        foreach (var e in errors)
        //            ModelState.AddModelError("", Service.ErrorMsgToRussian(e));
        //    }
        //    return View(userPassword);
        //}

        //[HttpPost]
        //public ActionResult SendPasswordEmail(string nameOrEmail)
        //{
        //    var result = ServiceProxySingleton.Instance.CreateTemporaryCode(new TemporaryCodeArg{NameOrEmail = nameOrEmail});
        //    if (result.IsError()) return Json(result);
        //    var helper = new EmailController("ChangePasswordEmail", result.AttachedObject);
        //    helper.SendPasswordEmail();
        //    TempData["SuccessMessage"] = Messages.EmailSentPasswordReset;
        //    return new EmptyResult();
        //}
        //#endregion


        #region Additional Methods

        private const int VIEW_COMPANY_NAME_LENGTH = 50;
        private const string VIEW_COMPANY_NAME_POSTFIX = "...";
        private string GetViewCompanyName(string companyName)
        {
            return !string.IsNullOrEmpty(companyName) && companyName.Length > VIEW_COMPANY_NAME_LENGTH ?
                companyName.Substring(0, VIEW_COMPANY_NAME_LENGTH) + VIEW_COMPANY_NAME_POSTFIX
                : companyName;
        }
        #endregion
    }
}
