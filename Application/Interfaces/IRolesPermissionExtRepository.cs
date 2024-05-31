using Application.Models;

namespace Application.Interfaces
{
    public interface IRolesPermissionExtRepository
    {
        Task<IEnumerable<RolePermissionExt>> GetRolePermissionExtsWithTenant(short tenantID);
    }
}
