using Application.Interfaces;
using Application.Models;
using Application.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class TasksOnlineAssignedRepository(WorkDbContext context) : ITasksOnlineAssignedRepository
    {
        private readonly WorkDbContext _context = context;

        public async Task<IEnumerable<TaskOnlineAssigned>> GetOnlineAssignedTasksWithTenant(short tenantID)
        {
            return await _context.TasksOnlineAssigned
                .Where(toa => toa.TenantID == tenantID && toa.AssignedTo != null)
                .ToListAsync();
        }
    }
}
