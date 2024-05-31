using Application.Models;

namespace Application.Interfaces
{
    public interface ITasksOnlineAssignedRepository
    {
        Task<IEnumerable<TaskOnlineAssigned>> GetOnlineAssignedTasksWithTenant(short tenantID);
    }
}
