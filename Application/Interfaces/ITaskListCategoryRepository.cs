using Application.Models;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ITaskListCategoryRepository
    {
        Task<IEnumerable<TaskListCategory>> GetTaskListCategoriesWhereIdInList(IEnumerable<byte> comparisonList);
    }
}
