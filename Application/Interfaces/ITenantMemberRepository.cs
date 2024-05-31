using Application.Models;

namespace Application.Interfaces
{
    public interface ITenantMemberRepository
    {
        Task<IEnumerable<TenantMember>> GetAllTenantMembers();
    }
}
