using Demo.Api.Dtos;
using Demo.Api.InputModels;
using Demo.Api.Mapping;
using Demo.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeMeasurer;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ILogger<ProjectsController> _logger;
        private readonly ITimeMeasurerFactory _timeMeasurer;
        private IProjectService projectService;
        private ILogger<ProjectsController> fakeLogger;

        public ProjectsController(IProjectService projectService , ILogger<ProjectsController> logger , ITimeMeasurerFactory timeMeasurer)
        {
            _projectService = projectService;
            _logger = logger;
            _timeMeasurer = timeMeasurer;
        }

        public ProjectsController(IProjectService projectService, ILogger<ProjectsController> fakeLogger)
        {
            this.projectService = projectService;
            this.fakeLogger = fakeLogger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get( [FromRoute] Guid id , CancellationToken token)
        {
            var result = await _projectService.GetDtoByIdAsync(id , token);

            if(result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            IEnumerable<ProjectDto> projects = null;

            using (var measurer1 = _timeMeasurer.Create())
            {
                measurer1.LogginMessage = " Total time is ";

                using (var measurer2 = _timeMeasurer.Create())
                {
                    measurer2.LogginMessage = "measuring get all async";
                    projects = await _projectService.GetAllAsync(token);
                }

                using (var measurer2 = _timeMeasurer.Create())
                { 
                    measurer2.LogginMessage = "Loggin the thread sleeping";
                    Thread.Sleep(5000);
                }
            }
            return Ok(projects);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id , CancellationToken token)
        {
            var project = await _projectService.GetByIdAsync(id, token);

            if(project == null)
            {
                return NotFound();
            }
            await _projectService.DeleteAsync(project, token);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] ProjectInputModel inputModel , CancellationToken token)
        {
            var project = await _projectService.GetByIdAsync( id, token); 

            if(project == null) 
            {
                return NotFound();
            }
            var projectDto = await _projectService.UpdateAsync(project, inputModel , token);
            return Ok(projectDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProjectInputModel newProject , CancellationToken token)
        {
           var project = await _projectService.AddAsync(newProject, token);
           return Ok(project.ToDto());
        }

        [HttpPost("addTasks")]
        public async Task<IActionResult> AddTasks([FromRoute] Guid projectId , [FromBody] IEnumerable<Guid> taskIds , CancellationToken token)
        {
            var project = await _projectService.GetByIdAsync(projectId, token);

            if(project == null)
            {
                return NotFound();
            }

            var projectDto = await _projectService.AddTasksToProjectAsync(project, taskIds, token);

            if(projectDto == null)
            {
                return BadRequest();
            }

            return Ok(projectDto);
        }

        [HttpPost("removeTasks")]
        public async Task<IActionResult> RemoveTasks([FromRoute] Guid projectId ,[FromBody] IEnumerable<Guid> tasksIds , CancellationToken token)
        {


            var project = await _projectService.GetByIdAsync(projectId, token);

            if (project == null)
            {
                return BadRequest();
            }

            var projectDto = await _projectService.RemoveTasksFromProjectAsync(project, tasksIds, token);

            return Ok(projectDto);
        }

    }
}
