using System.Collections.Generic;
using System.ComponentModel;

namespace CommonClasses
{
    public enum AuthenticationType
    {
        Native = 1,
        ActiveDirectory = 2
    }

    public enum AccessComponent
    {
        None = 0,
        Payrolls = 1,
        Transactions = 2, 
        Calculations = 3,
        BankPayrolls = 4,
        Acts = 5,
        Employees = 6,
        IndividPayroll = 7,
        Settings = 9,
        Currencies = 10,
        Company = 11,
        FinanceKey = 12,
        PositionCategories = 13,
        PositionLevels = 14,
        Users = 15,
        Roles = 16,
        Periods = 17,
        Duties = 18,
        TransactionTypes = 19,
        Positions = 20,
        Reports = 21
    }

    public enum AccessLevel
    {
        None = 1, 
        Read = 2, 
        ReadWrite = 3
    }

    public static class Constants
    {
        public const string DevToken = "_devtoken_";

        #region Session keys
        public const string SESSION_PERIODS = "SessionPeriods";
        public const string SESSION_IS_KEY_ENTERED = "FinanceKeyIsEntered";
        public const string SESSION_AUTH_INFO = "AuthToken";
        public const string SESSION_PERMISSIONS = "Permissions";
        public const string SESSION_COMPANY_ID = "CompanyId";
        public const string SESSION_VIEW_COMPANY_NAME = "CompanyName";
        public const string SESSION_COMPANY_HAS_KEY = "CompanyHasKey";
        public const string SESSION_USER_NAME = "UserName";
        public const string SESSION_FORCED_LOGOUT = "ForcedOut";
        public const string SESSION_LAST_LOGGED_COMPANY = "LastLoggedCompany";
        public const string SESSION_TRANSACTION_TYPES = "TransactionTypes";
        public const string SESSION_TRANSACTION_TYPES_VIEW = "TransactionTypesView";
        public const string SESSION_STORED_ALL_POSITIONS = "StoredAllPositions";
        public const string SESSION_COMPANY_USERS = "CompanyUsers";
        public const string SESSION_COMPANY_ROLES = "CompanyRoles";
        public const string SESSION_CURRENCY_CLASSES = "CurrencyClasses";
        public const string SESSION_REPORT_FILE = "GeneratedReport";
        #endregion 

        #region Predefined
        public static readonly List<int> ComponentsForGuest = new List<int>
        {
            (int)AccessComponent.Payrolls,
            (int)AccessComponent.BankPayrolls,
            (int)AccessComponent.Acts,
            (int)AccessComponent.Employees,
        };

        public static readonly List<int> DisabledComponentsForAdmin = new List<int>
        {
            (int)AccessComponent.Roles,
            (int)AccessComponent.Users
        };

        #endregion
    }


    public static class RegexExpressions
    {
        public const string EmailRegex = @"[A-Za-z\d]+([-+.'][A-Za-z\d]+)*@[A-Za-z\d]+([-.][A-Za-z\d]+)*\.[A-Za-z\d]+([-.][A-Za-z\d]+)*";
    }
}
