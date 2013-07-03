using CommonClasses.Helpers;

namespace CommonClasses.MethodArguments
{
    public class LogonArg
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string FinanceKey { get; set; }
        public int DefaultInstanceId { get; set; }
        
        public LogonArg()
        {
        }

        public LogonArg(string login, string passwordHash, string salt, string financeKey)
        {
            Login = login;
            PasswordHash = passwordHash;
            Salt = salt;
            FinanceKey = financeKey;
            DefaultInstanceId = AppConfiguration.DefaultCompanyId;
        }
    }
}