using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.InfoClasses;
using CommonClasses.MethodResults;

namespace BusinessLayer.Managers
{
    public class UserManager: CommonManager
    {
        public MethodResult<List<UserInstanceInfo>> GetUserInstanceList()
        {
            return new MethodResult<List<UserInstanceInfo>>(Db.GetUserInstanceList());
        }
    }
}
