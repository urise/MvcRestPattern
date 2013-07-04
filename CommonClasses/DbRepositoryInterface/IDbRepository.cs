using System;
using System.Collections.Generic;
using CommonClasses.DbClasses;
using CommonClasses.InfoClasses;
using CommonClasses.MethodArguments;
using CommonClasses.Roles;

namespace CommonClasses.DbRepositoryInterface
{
    public interface IDbRepository: IDisposable
    {
        int InstanceId { get; }
        int UserId { get; }

        void SetInstanceId(int instanceId);
        void SetAuthInfo(AuthInfo authInfo);
        //IDbTransaction BeginTransaction();

        int Save<T>(T obj) where T : class, IMapping;
        int SaveInstanceUsage(InstanceUsage instanceUsage);
        int SaveTemporaryCode(TemporaryCode temporaryCode);

        void DeleteTemporaryCode(int temporaryCodeId);

        User GetUserByKey(string key);
        TemporaryCode GetTemporaryCodeByUserId(int userId);
        User GetAndAuthenticateUser(LogonArg arg);
        User GetUserByLogin(string login);
        User GetUserByEmail(string email);
        string GetUserNameByCode(string code);
        int GetLastUsedInstanceId(int userId);
        bool CheckIfUserLinkedToInstance(int userId, int instanceId);
        UserAccess GetUserAccess(int userId);
        Instance GetInstanceById(int id);
        IList<Instance> GetUserInstances(int userId);

        bool LoginIsNotUnique(string login);
        bool EmailIsNotUnique(string email, int userId = 0);
    }
}
