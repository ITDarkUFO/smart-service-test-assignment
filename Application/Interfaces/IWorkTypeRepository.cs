using Application.Models;

namespace Application.Interfaces
{
    public interface IWorkTypeRepository
    {
        Task<IEnumerable<WorkType>> GetAllWorkTypes();
    }
}
