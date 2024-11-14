using TaskTrackr.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskTrackr.Server.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project> GetProjectByIdAsync(int id);
        Task CreateProjectAsync(Project project);
        Task<bool> UpdateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(int id);
    }
}
