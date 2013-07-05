using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses;
using CommonClasses.DbClasses;
using CommonClasses.Helpers;
using CommonClasses.MethodResults;

namespace BusinessLayer.Managers
{
    public class InstanceManager: CommonManager
    {
        #region Create Save Company

        public MethodResult<int> CreateCompany(string instanceName)
        {
            var dbTran = Db.BeginTransaction();
            try
            {
                var instance = new Instance
                {
                    InstanceName = instanceName
                };
                var validateError = ValidateInstance(instance);
                if (!string.IsNullOrEmpty(validateError))
                    return new MethodResult<int> { ErrorMessage = validateError };

                Db.Save(instance);
                Db.SetInstanceId(instance.InstanceId);

              //  var adminRoleId = InsertSystemRoles();
                Db.CreateUserInstance();
                //Db.AddUserToRole(adminRoleId);
                
                dbTran.Commit();
                return new MethodResult<int>(instance.InstanceId);
            }
            catch
            {
                dbTran.Rollback();
                throw;
            }
        }
        
        public string ValidateInstance(Instance instance)
        {
            if (string.IsNullOrEmpty(instance.InstanceName))
                return Messages.EmptyCompanyName;
            if (Db.IsExistInstanceName(instance.InstanceName))
                return Messages.ExistsCompanyName;
            return null;
        }
        /*

                private int InsertSystemRoles()
                {
                    var adminRoleId = InsertAdminRoleAndAccess();
                    InsertGuestRoleAndAccess();
                    return adminRoleId;
                }

                private int InsertAdminRoleAndAccess()
                {
                    var adminRole = new RoleDb
                    {
                        CompanyId = Db.CompanyId,
                        Type = (int)SystemRoles.Administrator,
                        Name = SystemRoles.Administrator.GetDescription()
                    };
                    Db.SaveRole(adminRole);
                    foreach (var component in Enum.GetValues(typeof(AccessComponent)))
                    {
                        if ((int)component == (int)AccessComponent.None)
                            continue;
                        var componentsToRole = new ComponentsToRoleDb
                        {
                            CompanyId = Db.CompanyId,
                            AccessLevel = (int)AccessLevel.ReadWrite,
                            ComponentId = (int)component,
                            RoleId = adminRole.RoleId
                        };
                        Db.SaveComponentsToRole(componentsToRole);
                    }
                    return adminRole.RoleId;
                }

                private void InsertGuestRoleAndAccess()
                {
                    var guestRole = new RoleDb
                    {
                        CompanyId = Db.CompanyId,
                        Type = (int)SystemRoles.Guest,
                        Name = SystemRoles.Guest.GetDescription()
                    };
                    Db.SaveRole(guestRole);
                    foreach (var componentId in Constants.ComponentsForGuest)
                    {
                        var componentsToRole = new ComponentsToRoleDb
                        {
                            CompanyId = Db.CompanyId,
                            AccessLevel = (int)AccessLevel.Read,
                            ComponentId = componentId,
                            RoleId = guestRole.RoleId
                        };
                        Db.SaveComponentsToRole(componentsToRole);
                    }
                }

                private int GetSystemRole(SystemRoles roleType)
                {
                    if (roleType == SystemRoles.None)
                        throw new Exception();
                    var roleId = Db.GetSystemRoleId(roleType);
                    if (!roleId.HasValue)
                        throw new Exception();
                    return roleId.Value;
                }
                */
        #endregion
    }
}
