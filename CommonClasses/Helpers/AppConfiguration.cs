using System;
using System.Configuration;

namespace CommonClasses.Helpers
{
    public static class AppConfiguration
    {
        public const int HoursPerDay = 8;
        public const int HourRateDecimals = 2;
        public const int ValueLimit = 100000000;
        public const decimal OvertimeKoef = 1.5m;
        public const decimal VacationCompensationKoef = 0.5m;
        public const int MaxVacationCompensationDays = 5;
        public const int AverageDaysPerMonth = 21;
        public const int PlanedCountDay = 10;
        public const decimal CommonTaxCoef = 1.0525m;
        public const int NationalCurrencyId = 3;

        public static int ServiceMethodDelay
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings["ServiceMethodDelay"] ?? "0");
            }
        }

        public static string HudsonBuildNumber
        {
            get { return ConfigurationManager.AppSettings["HudsonBuildNumber"]; }
        }

        public static bool ShowPayrollsAtStartup
        {
            get { return Boolean.Parse(ConfigurationManager.AppSettings["ShowPayrollsAtStartup"]); }
        }

        public static string RestServiceUrl
        {
            get { return ConfigurationManager.AppSettings["RestServiceUrl"]; }
        }

        public static AuthenticationType AuthenticationMethod
        {
            get
            {
                AuthenticationType result;
                string methodName = ConfigurationManager.AppSettings["AuthenticationMethod"];
                if (string.IsNullOrEmpty(methodName)) return AuthenticationType.Native;

                if (! Enum.TryParse(methodName, true, out result))
                    throw new Exception("Unknown authentication method in web.config");
                return result;
            }
        }

        public static string LDAPServer
        {
            get { return ConfigurationManager.AppSettings["LDAPServer"]; }
        }

        public static int DefaultCompanyId
        {
            get
            {
                int defaultCompanyId;
                int.TryParse(ConfigurationManager.AppSettings["DefaultCompanyId"], out defaultCompanyId);
                return defaultCompanyId;
            }
        }

        public static int PasswordLinkTtl
        {
            get 
            { 
                int passTtl;
                int.TryParse(ConfigurationManager.AppSettings["PasswordLinkTtl"], out passTtl);
                return passTtl;
            }
        }

        public static string AdminEmailAddress
        {
            get
            {
                 return ConfigurationManager.AppSettings["AdminEmailAddress"];
            }
        }
    }
}
