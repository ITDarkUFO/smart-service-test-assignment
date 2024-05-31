using Application.Interfaces;
using Application.Models;
using Application.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class UserDistrictRepository(AdminDbContext context) : IUserDistrictRepository
    {
        private readonly AdminDbContext _context = context;

        public async Task<IEnumerable<UserDistrict>> GetAllUserDistricts()
        {
            return await _context.UserDistricts
                .Where(ud => ud.Deleted == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserDistrict>> GetUserDistrictsWithTenant(short tenantID)
        {
            return await _context.UserDistricts
                .Where(ud => ud.TenantID == tenantID && ud.Deleted == null)
                .ToListAsync();
        }
    }
}
