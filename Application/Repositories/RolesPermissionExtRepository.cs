using Application.Interfaces;
using Application.Models;
using Application.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class RolesPermissionExtRepository(AdminDbContext context) : IRolesPermissionExtRepository
    {
        private readonly AdminDbContext _context = context;

        public async Task<IEnumerable<RolePermissionExt>> GetRolePermissionExtsWithTenant(short tenantID)
        {
            return await _context.RolesPermissionExt
                .Where(rpe => rpe.TenantID == tenantID && rpe.Deleted == null)
                .ToListAsync();
        }
    }
}
