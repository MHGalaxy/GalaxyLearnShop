using GalaxyLearn.Core.DTOs.Role;
using GalaxyLearn.DataLayer.Entities.Permission;
using GalaxyLearn.DataLayer.Entities.User;

namespace GalaxyLearn.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Roles

        List<Role> GetRoles();
        Role GetRoleById(int roleId);
        EditRoleViewModel GetRoleForEdit(int roleId);
        int AddRole(Role role);
        void EditRole(Role role);
        void DeleteRole(int roleId);
        void AddRolesToUser(List<int> roleIds, int userId);
        void AddNormalUserRole(int userId);
        void EditUserRoles(List<int> roleIds, int userId);

        #endregion

        #region Permission

        List<Permission> GetAllPermissions();
        void AddPermissionsToRole(int roleId, List<int>? permissionIds);
        void EditPermissionToRole(int roleId, List<int>? permissionIds);
        bool CheckPermission(int permissionId, int userId);
        #endregion
    }
}
