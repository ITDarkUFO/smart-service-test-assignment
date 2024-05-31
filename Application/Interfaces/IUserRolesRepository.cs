using Application.Models;

namespace Application.Interfaces
{
    public interface IUserRolesRepository
    {
        Task<IEnumerable<UserRole>> GetUserRolesWithTenant(short tenantId);
    }
}
