using Application.Interfaces;
using Application.Models;
using Application.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class TenantMemberRepository(AdminDbContext context) : ITenantMemberRepository
    {
        private readonly AdminDbContext _context = context;

        public async Task<IEnumerable<TenantMember>> GetAllTenantMembers()
        {
            return await _context.TenantMembers.ToListAsync();
        }
    }
}
