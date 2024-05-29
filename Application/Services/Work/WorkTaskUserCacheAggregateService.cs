using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Work
{
    public class WorkTaskUserCacheAggregateService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        private readonly bool _isOwnerOnly = false;
        private readonly byte _assignedTo = 2;
        private readonly byte _districtAvailable = 13;
        private readonly byte _userWorkType = 16;
        private readonly byte _allTaskAvailable = 19;
        private readonly byte _createdBy = 20;

        public async Task<IActionResult> TaskUserCacheAggregate(short tenantID)
        {
            DateTime now = DateTime.UtcNow;


            return null;
        }
    }
}
