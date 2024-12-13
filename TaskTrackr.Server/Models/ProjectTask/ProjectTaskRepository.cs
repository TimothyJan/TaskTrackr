using Microsoft.EntityFrameworkCore;
using TaskTrackr.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskTrackr.Server.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly TaskTrackrDbContext _context;

        public ProjectTaskRepository(TaskTrackrDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectTask>> GetAllTasksAsync()
        {
            return await _context.ProjectTasks.ToListAsync();
        }

        public async Task<ProjectTask> GetTaskByIdAsync(int id)
        {
            return await _context.ProjectTasks.FindAsync(id);
        }

        public async Task CreateTaskAsync(ProjectTask task)
        {
            await _context.ProjectTasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTaskAsync(ProjectTask task)
        {
            _context.Entry(task).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task == null) return false;

            _context.ProjectTasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<int>> GetTaskIdsByProjectIdAsync(int projectId)
        {
            return await _context.ProjectTasks
                .Where(task => task.ProjectId == projectId)
                .Select(task => task.ProjectTaskId)
                .ToListAsync();
        }

    }
}
