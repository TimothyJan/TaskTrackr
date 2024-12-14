using Microsoft.EntityFrameworkCore;
using TaskTrackr.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskTrackr.Server.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskTrackrDbContext _context;

        public ProjectRepository(TaskTrackrDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task CreateProjectAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateProjectAsync(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
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

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<int>> GetAllProjectIdsAsync()
        {
            return await _context.Projects.Select(p => p.ProjectId).ToListAsync();
        }
    }
}