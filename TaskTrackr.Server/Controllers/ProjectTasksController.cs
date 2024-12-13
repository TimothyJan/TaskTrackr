using Microsoft.AspNetCore.Mvc;
using TaskTrackr.Server.Models;
using TaskTrackr.Server.Repositories;

namespace TaskTrackr.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskRepository _taskRepository;

        public ProjectTaskController(IProjectTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET: api/ProjectTask
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetTasks()
        {
            return Ok(await _taskRepository.GetAllTasksAsync());
        }

        // GET: api/ProjectTask/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> GetTask(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // POST: api/ProjectTask
        [HttpPost]
        public async Task<ActionResult> CreateTask(ProjectTask projecTask)
        {
            await _taskRepository.CreateTaskAsync(projecTask);
            return CreatedAtAction(nameof(GetTask), new { id = projecTask.ProjectTaskId }, projecTask);
        }

        // PUT: api/ProjectTask/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, ProjectTask projecTask)
        {
            if (id != projecTask.ProjectTaskId)
            {
                return BadRequest();
            }

            var success = await _taskRepository.UpdateTaskAsync(projecTask);
            return success ? NoContent() : NotFound();
        }

        // DELETE: api/ProjectTask/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _taskRepository.DeleteTaskAsync(id);
            return success ? NoContent() : NotFound();
        }

        // GET: api/ProjectTask/GetTaskIdsByProjectId/{projectId}
        [HttpGet("GetTaskIdsByProjectId/{projectId}")]
        public async Task<ActionResult<IEnumerable<int>>> GetTaskIdsByProjectId(int projectId)
        {
            var taskIds = await _taskRepository.GetTaskIdsByProjectIdAsync(projectId);
            if (!taskIds.Any())
            {
                return NotFound($"No tasks found for ProjectId: {projectId}");
            }
            return Ok(taskIds);
        }

    }
}
