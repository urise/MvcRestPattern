using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.DbClasses;
using CommonClasses.MethodArguments;

namespace CommonClasses.DbRepositoryInterface
{
    public interface IDbRepository: IDisposable
    {
        User GetAndAuthenticateUser(LogonArg arg);
        User GetUserByLogin(string login);
        int GetLastUsedCompanyId(int userId);
    }
}
