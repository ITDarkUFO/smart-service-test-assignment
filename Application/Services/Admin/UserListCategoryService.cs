using Application.Interfaces;
using Application.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Admin
{
    public class UserListCategoryService(
        IUserRolesRepository userRolesRepository, 
        IRolesPermissionExtRepository rolesPermissionExtRepository)
    {
        private readonly IUserRolesRepository _userRolesRepository = userRolesRepository;
        private readonly IRolesPermissionExtRepository _rolesPermissionExtRepository = rolesPermissionExtRepository;

        public async Task<List<UserTaskListCategoryDTO>> UserListCategoryGet
            (short tenantID, List<ListCategoryDTO> listCategories, List<UserDTO> users)
        {
            var usersJoinCategories = users
                .SelectMany(u => listCategories
                    .Select(lc => new { User = u, ListCategory = lc }));

            var rolePermissionExtWithTenant = 
                await _rolesPermissionExtRepository.GetRolePermissionExtsWithTenant(tenantID);

            var userRolesWithTenant = await _userRolesRepository.GetUserRolesWithTenant(tenantID);

            var usersWithTenant = usersJoinCategories
                .Where(ujc => userRolesWithTenant
                    .Where(urt => rolePermissionExtWithTenant
                        .Where(rpe => rpe.Permissionextid == ujc.ListCategory.Permissionextid)
                        .Select(rpe => rpe.RoleID)
                        .Contains(urt.RoleID))
                    .Select(urt => urt.UserID)
                    .Contains(ujc.User.ID)).ToList();

            var usersWithNoPermissions = usersJoinCategories
                .Where(o => o.ListCategory.Permissionextid == null)
                .ToList();

            return usersWithTenant.Concat(usersWithNoPermissions)
                .Select(o => new UserTaskListCategoryDTO
                {
                    UserID = o.User.ID,
                    TaskListCategoryID = o.ListCategory.ID
                })
                .ToList();
        }
    }
}
