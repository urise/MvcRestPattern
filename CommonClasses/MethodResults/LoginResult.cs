using CommonClasses.Roles;

namespace CommonClasses.MethodResults
{
    public class LoginResult: BaseResult
    {
        public string Token { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool FinanceKeyIsEntered { get; set; }
        public bool CompanyHasKey { get; set; }
        public int LastUsedCompanyId { get; set; }
        public UserAccess Access { get; set; }
    }
}