using CommonClasses.Roles;

namespace CommonClasses.MethodResults
{
    public class LoginResult: BaseResult
    {
        public string Token { get; set; }
        public int InstanceId { get; set; }
        public string InstanceName { get; set; }
        public bool FinanceKeyIsEntered { get; set; }
        public bool CompanyHasKey { get; set; }
        public int LastUsedInstanceId { get; set; }
        public UserAccess Access { get; set; }
    }
}