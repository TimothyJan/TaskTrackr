using Microsoft.AspNetCore.Mvc;
using TaskTrackr.Server.Models;
using TaskTrackr.Server.Repositories;

namespace TaskTrackr.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return Ok(await _projectRepository.GetAllProjectsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProject(Project project)
        {
            await _projectRepository.CreateProjectAsync(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            var success = await _projectRepository.UpdateProjectAsync(project);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var success = await _projectRepository.DeleteProjectAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("getAllProjectIds")]
        public async Task<ActionResult<IEnumerable<int>>> GetListOfProjectIds()
        {
            var projectIds = await _projectRepository.GetAllProjectIdsAsync();
            return Ok(projectIds);
        }
    }
}
