using GalaxyLearn.Core.DTOs.Role;
using GalaxyLearn.Core.DTOs.UserAdminPanel;
using GalaxyLearn.Core.Enums;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.DataLayer.Context;
using GalaxyLearn.DataLayer.Entities.Permission;
using GalaxyLearn.DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace GalaxyLearn.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly GalaxyContext _context;
        public PermissionService(GalaxyContext context)
        {
            _context = context;
        }

        #region Roles

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public EditRoleViewModel GetRoleForEdit(int roleId)
        {
            return _context.Roles
                .Include(r => r.RolePermissions)
                .Where(r => r.RoleId == roleId)
                .Select(r => new EditRoleViewModel()
                {
                    RoleId = roleId,
                    RoleName = r.RoleName,
                    RoleTitle = r.RoleTitle,
                    PermissionIds = r.RolePermissions.Select(r => r.PermissionId).ToList()
                }).First();
        }

        public void EditRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void DeleteRole(int roleId)
        {
            _context.Roles.Remove(_context.Roles.Find(roleId));
            _context.SaveChanges();
        }

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (int roleId in roleIds)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    UserId = userId,
                    RoleId = roleId
                });
            }

            _context.SaveChanges();
        }

        public void AddNormalUserRole(int userId)
        {
            var isNormalRole = _context.UserRoles.Where(ur => ur.UserId == userId && ur.RoleId == (int)Roles.NormalUser).Any();

            if (!isNormalRole)
            {
                _context.UserRoles.Add(new UserRole
                {
                    UserId = userId,
                    RoleId = (int)Roles.NormalUser
                });

                _context.SaveChanges();
            }
        }

        public void EditUserRoles(List<int> roleIds, int userId)
        {
            //Delete All User Roles
            _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .ToList()
                .ForEach(ur => _context.UserRoles.Remove(ur));

            //Add New Roles
            AddRolesToUser(roleIds, userId);
        }

        #endregion

        #region Permission

        public List<Permission> GetAllPermissions()
        {
            return _context.Permissions.ToList();
        }

        public void AddPermissionsToRole(int roleId, List<int>? permissionIds)
        {
            if (permissionIds is null) return;

            foreach (var permissionId in permissionIds)
            {
                _context.RolePermissions.Add(new RolePermission()
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                });
            }
            _context.SaveChanges();
        }

        public void EditPermissionToRole(int roleId, List<int>? permissionIds)
        {
            //Delete All Permissions
            _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .ToList()
                .ForEach(rp => _context.RolePermissions.Remove(rp));

            //Add New Permissions
            AddPermissionsToRole(roleId, permissionIds);
        }

        public bool CheckPermission(int permissionId, int userId)
        {
            // Get User Roles
            var userRoleIds = _context.UserRoles
                .Where(r => r.UserId == userId)
                .Select(r => r.RoleId)
                .ToList();

            if (!userRoleIds.Any()) return false;

            // Get Permission Roles
            var permissionRoleIds = _context.RolePermissions
                .Where(p => p.PermissionId == permissionId)
                .Select(p => p.RoleId)
                .ToList();

            // Check permissionRoleIds has any of userRoleIds:
            return userRoleIds.Intersect(permissionRoleIds).Any();
            //return permissionRoleIds.Any(p => userRoleIds.Contains(p)); 
        }
        #endregion
    }
}
