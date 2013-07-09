using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CommonClasses;
using CommonClasses.InfoClasses;
using ServiceProxy;
using WebSite.Helpers;

namespace WebSite.Controllers
{
    [SessionAuthorize]
    public class UsersController : Controller
    {
        #region  Properties and additional methods

        private List<UserInstanceInfo> StoredInstanceUsers
        {
            get { return Session[Constants.SESSION_INSTANCE_USERS] as List<UserInstanceInfo>; }
            set { Session[Constants.SESSION_INSTANCE_USERS] = value; }
        }

        private List<UserInstanceInfo> GetInstanceUsers(string searchString)
        {
            var list = StoredInstanceUsers;

            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(tt => tt.UserName.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return list;
        }
        #endregion

        [HttpGet]
        [Permissions(AccessComponent.Users, AccessLevel.Read)]
        public ActionResult Users()
        {
            var searchString = (string)TempData["Filters"];
            if (StoredInstanceUsers == null)
            {
                var result = GetCompanyUsersResult();
                if (StoredInstanceUsers == null) return result;
            }
            var model = GetInstanceUsers(searchString);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        private ActionResult GetCompanyUsersResult()
        {
            var response = ServiceProxySingleton.Instance.GetUserInstanceList();
            if (response.IsNotLoggedIn()) return SessionHelper.ClearSession();
            if (response.IsAccessDenied()) return RedirectToAction("AccessError", "Error");
            StoredInstanceUsers = response.AttachedObject;
            return null;
        }


        [HttpPost]
        [Permissions(AccessComponent.Users, AccessLevel.Read)]
        public ActionResult CompanyUsersList(string searchString)
        {
            TempData["Filters"] = searchString;
            return RedirectToAction("CompanyUsersList");
        }

//        [HttpPost]
//        public ActionResult Update(string userName)
//        {
//            var response = ServiceProxySingleton.Instance.SaveUserToCompany(userName);
//            if (response.IsNotLoggedIn()) return SessionHelper.ClearSession();
//            if (response.IsError()) return Json(response);
//            SessionHelper.ClearUserRolesFromSession();
//            return Json(new { response.Id, response.Value });
//        }
//
//        [HttpPost]
//        public ActionResult Delete(int id)
//        {
//            var response = ServiceProxySingleton.Instance.DeleteUserToCompany(new DeleteArg { Id = id });
//            if (response.IsNotLoggedIn()) return SessionHelper.ClearSession();
//            if (response.IsError()) return Json(response);
//            SessionHelper.ClearUserRolesFromSession();
//            return new EmptyResult();
//        }
    }
}
