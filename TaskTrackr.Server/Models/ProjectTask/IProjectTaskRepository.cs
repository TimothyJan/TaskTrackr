using TaskTrackr.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskTrackr.Server.Repositories
{
    public interface IProjectTaskRepository
    {
        Task<IEnumerable<ProjectTask>> GetAllTasksAsync();
        Task<ProjectTask> GetTaskByIdAsync(int id);
        Task CreateTaskAsync(ProjectTask task);
        Task<bool> UpdateTaskAsync(ProjectTask task);
        Task<bool> DeleteTaskAsync(int id);
        Task<IEnumerable<int>> GetTaskIdsByProjectIdAsync(int projectId);

    }
}
