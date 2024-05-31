using Application.Models;

namespace Application.Interfaces
{
    public interface IUserDistrictRepository
    {
        Task<IEnumerable<UserDistrict>> GetAllUserDistricts();

        Task<IEnumerable<UserDistrict>> GetUserDistrictsWithTenant(short tenantID);
    }
}
