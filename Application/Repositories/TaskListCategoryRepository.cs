using Application.Interfaces;
using Application.Models;
using Application.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class TaskListCategoryRepository(WorkDbContext context) : ITaskListCategoryRepository
    {
        private readonly WorkDbContext _context = context;

        public async Task<IEnumerable<TaskListCategory>> GetTaskListCategoriesWhereIdInList
            (IEnumerable<byte> comparisonList)
        {
            if (comparisonList == null)
                return [];

            if (!comparisonList.Any())
                return [];

            return await _context.TaskListCategories
                .Where(tlc => comparisonList.Contains(tlc.ID))
                .ToListAsync();
        }
    }
}
