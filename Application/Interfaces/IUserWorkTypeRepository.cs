using Application.Models;

namespace Application.Interfaces
{
    public interface IUserWorkTypeRepository
    {
        Task<IEnumerable<UserWorkType>> GetAllUserWorkTypes();
    }
}
