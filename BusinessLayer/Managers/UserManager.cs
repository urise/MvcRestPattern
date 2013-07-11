using System;
using System.Collections.Generic;
using CommonClasses;
using CommonClasses.InfoClasses;
using CommonClasses.MethodResults;
using Interfaces.Enums;

namespace BusinessLayer.Managers
{
    public class UserManager: CommonManager
    {
        #region Manage UserInstance
        public MethodResult<List<string>> GetUserInstanceList()
        {
            return new MethodResult<List<string>>(Db.GetUserInstanceList());
        }

        public BaseResult SaveUserInstance(string loginOrEmail)
        {
            var isEmailEntered = loginOrEmail.Contains("@");
            var user = isEmailEntered ? Db.GetUserByEmail(loginOrEmail) : Db.GetUserByLogin(loginOrEmail);
            if (user == null)
                return new MethodResult<Tuple<int, string>> { ErrorMessage = isEmailEntered ? Messages.UserNotFoundByEmail : Messages.UserNotFoundByLogin };
            if (Db.CheckIfUserLinkedToInstance(user.UserId))
                return new MethodResult<Tuple<int, string>> { ErrorMessage = Messages.UserInstanceAlreadyExist };

            var roleId = Db.GetSystemRoleId(SystemRoles.Guest);
            Db.AddUserInstance(user.UserId);
            Db.AddUserRole(roleId, user.UserId);
            return new BaseResult();
        }

        public BaseResult DeleteUserInstance(string userName)
        {
            var userCompany = Db.GetUserInstance(userName);
            if (userCompany == null)
                return new BaseResult { ErrorMessage = Messages.UserInstanceNotFound };
            Db.DeleteUserRoles(userCompany.UserId, string.Empty);
            Db.Delete(userCompany);
            return new BaseResult();
        }

        #endregion
    }
}
