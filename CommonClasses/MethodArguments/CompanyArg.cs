namespace CommonClasses.MethodArguments
{
    public class CompanyArg
    {
        public int CompanyId { get; set; }
        public string Salt { get; set; }
        public string FinanceKey { get; set; }


        public CompanyArg()
        {
        }

        public CompanyArg(int companyId, string salt, string financeKey)
        {
            CompanyId = companyId;
            Salt = salt;
            FinanceKey = financeKey;
        }
    }
}