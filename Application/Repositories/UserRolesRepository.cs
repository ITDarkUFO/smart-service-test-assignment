using Application.Interfaces;
using Application.Models;
using Application.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class UserRolesRepository(AdminDbContext context) : IUserRolesRepository
    {
        private readonly AdminDbContext _context = context;

        public async Task<IEnumerable<UserRole>> GetUserRolesWithTenant(short tenantID)
        {
            return await _context.UserRoles
                .Where(urt => urt.TenantID == tenantID && urt.Deleted == null)
                .ToListAsync();
        }
    }
}
