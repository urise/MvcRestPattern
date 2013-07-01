using System.Web;
using System.Web.Mvc;
using CommonClasses;
using CommonClasses.Roles;

//using CommonClasses.Roles;

namespace WebSite.Helpers
{
    public static class SessionHelper
    {
        public static bool IsFinanceKeyEntered
        {
            get
            {
                var session = HttpContext.Current.Session;
                return session[Constants.SESSION_IS_KEY_ENTERED] != null && (bool) session[Constants.SESSION_IS_KEY_ENTERED];
            }
            set
            {
                var session = HttpContext.Current.Session;
                session[Constants.SESSION_IS_KEY_ENTERED] = value;
            }
        }

        public static bool IsAthorized()
        {
            var session = HttpContext.Current.Session;
            return session != null && session[Constants.SESSION_AUTH_INFO] != null;
        }

        public static bool IsCompanySelected()
        {
            var session = HttpContext.Current.Session;
            return session != null && session[Constants.SESSION_COMPANY_ID] != null && (int)session[Constants.SESSION_COMPANY_ID] != 0;
        }

        public static string UserName()
        {
            var session = HttpContext.Current.Session;
            var login = session[Constants.SESSION_USER_NAME];
            return (session != null && login != null) ? login.ToString() : null;
        }

        public static string CompanyName()
        {
            var session = HttpContext.Current.Session;
            var companyName = session[Constants.SESSION_VIEW_COMPANY_NAME];
            return (session != null && companyName != null) ? companyName.ToString() : null;
        }

        public static ActionResult ClearSession(string errorMessage = null)
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                session.Clear();
                session[Constants.SESSION_FORCED_LOGOUT] = errorMessage ?? Messages.SessionTimeOut;
            }
            return null;
        }

        public static void ClearCompanyInfoFromSession()
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                session[Constants.SESSION_COMPANY_ID] = null;
                session[Constants.SESSION_VIEW_COMPANY_NAME] = null;
                session[Constants.SESSION_PERIODS] = null;
                session[Constants.SESSION_TRANSACTION_TYPES] = null;
                session[Constants.SESSION_TRANSACTION_TYPES_VIEW] = null;
                session[Constants.SESSION_STORED_ALL_POSITIONS] = null;
                session[Constants.SESSION_COMPANY_USERS] = null;
                session[Constants.SESSION_COMPANY_ROLES] = null;
            }
        }

        public static void ClearPeriodsFromSession()
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                session[Constants.SESSION_PERIODS] = null;
            }
        }

        public static void ClearPositionsFromSession()
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                session[Constants.SESSION_STORED_ALL_POSITIONS] = null;
            }
        }

        public static void ClearTransactionTypesFromSession()
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                session[Constants.SESSION_TRANSACTION_TYPES] = null;
                session[Constants.SESSION_TRANSACTION_TYPES_VIEW] = null;
            }
        }

        public static void ClearUserRolesFromSession()
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                session[Constants.SESSION_COMPANY_USERS] = null;
                session[Constants.SESSION_COMPANY_ROLES] = null;
            }
        }

        public static UserAccess Permissions
        {
            get
            {
                var session = HttpContext.Current.Session;
                return session == null ? null : (UserAccess)session[Constants.SESSION_PERMISSIONS];
            }
            set
            {
                var session = HttpContext.Current.Session;
                session[Constants.SESSION_PERMISSIONS] = value;
            }
        }

        public static bool CompanyHasKey
        {
            get
            {
                var session = HttpContext.Current.Session;
                return session != null && (bool)session[Constants.SESSION_COMPANY_HAS_KEY];
            }
            set
            {
                var session = HttpContext.Current.Session;
                session[Constants.SESSION_COMPANY_HAS_KEY] = value;
            }
        }
    }
}