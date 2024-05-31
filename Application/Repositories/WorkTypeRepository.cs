using Application.Interfaces;
using Application.Models;
using Application.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class WorkTypeRepository(WorkDbContext context) : IWorkTypeRepository
    {
        private readonly WorkDbContext _context = context;

        public async Task<IEnumerable<WorkType>> GetAllWorkTypes()
        {
            return await _context.WorkTypes.ToListAsync();
        }
    }
}
