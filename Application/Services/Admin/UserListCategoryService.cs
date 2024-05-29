using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Admin
{
    public class UserListCategoryService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        //INFO: Неясно откуда поступают Users, тк таблица #User временная
        public async Task<List<UserCategoryDTO>> UserListCategoryGet
            (short tenantID, List<ListCategoryDTO> listCategories)
        {
            var users = await _context.Users.ToListAsync();

            var usersJoinCategories = users
                .SelectMany(u => listCategories
                    .Select(lc => new { User = u, ListCategory = lc }));

            var rolePermissionExtWithTenant = await _context.RolePermissionExts
                .Where(rpe => rpe.TenantID == tenantID && rpe.Deleted == null).ToListAsync();

            var userRolesWithTenant = await _context.UserRoles
                    .Where(urt => urt.TenantID == tenantID && urt.Deleted == null).ToListAsync();

            var usersWithTenant = usersJoinCategories
                .Where(ujc => userRolesWithTenant
                    .Where(urt => rolePermissionExtWithTenant
                        .Where(rpe => rpe.Permissionextid == ujc.ListCategory.Permissionextid)
                        .Select(rpe => rpe.RoleID)
                        .Contains(urt.RoleID))
                    .Select(urt => urt.UserID)
                    .Contains(ujc.User.ID)).ToList();

            var usersWithNoPermissions = usersJoinCategories
                .Where(o => o.ListCategory.Permissionextid == null).ToList();

            return usersWithTenant.Concat(usersWithNoPermissions)
                .Select(o => new UserCategoryDTO
                {
                    UserID = o.User.ID,
                    ListCategoryID = o.ListCategory.ID
                })
                .ToList();
        }
    }
}
