using Application.Interfaces;
using Application.Models;
using Application.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class UserWorkTypeRepository(PaDbContext context) : IUserWorkTypeRepository
    {
        private readonly PaDbContext _context = context;

        public async Task<IEnumerable<UserWorkType>> GetAllUserWorkTypes()
        {
            return await _context.UserWorkTypes.ToListAsync();
        }
    }
}
