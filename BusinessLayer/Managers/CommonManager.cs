using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.DbRepositoryInterface;

namespace BusinessLayer.Managers
{
    public class CommonManager: IDbManager
    {
        #region Constructors

        public CommonManager() { }

        public CommonManager(IDbRepository repository)
        {
            Db = repository;
        }

        #endregion

        public IDbRepository Db { get; protected set; }
    }
}
