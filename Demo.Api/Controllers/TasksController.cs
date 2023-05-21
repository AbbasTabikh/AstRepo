using Demo.Api.Dtos;
using Demo.Api.InputModels;
using Demo.Api.Services;
using Demo.TimeRecorder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IProjectService _projectService;
        private readonly ITimeRecorderService _recorderService;

        public TasksController(ITaskService taskService, IProjectService projectService , ITimeRecorderService recorderService)
        {
            _taskService = taskService;
            _projectService = projectService;
            _recorderService = recorderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id , CancellationToken token)
        {
            var task = await _taskService.GetDtoByIdAsync(id, token);

            if(task != null)
            {
                return Ok(task);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TaskInputModel newTask , CancellationToken token)
        {
            //check the provided projects Ids
            foreach (var projectId in newTask.Projects)
            {
                var project = await _projectService.GetByIdAsync(projectId , token);

                //project Id not found
                if(project == null)
                {
                    return NotFound();
                }
            }

            var task = await _taskService.AddAsync(newTask , token);

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id , CancellationToken token)
        {
            var task = await _taskService.GetByIdAsync(id, token);

            if(task == null)
            {
                return NotFound();
            }

            await _taskService.DeleteAsync(task, token);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectDto>> Update([FromRoute] Guid id , [FromBody] TaskInputModel newTask , CancellationToken token)
        {
            var task = await _taskService.GetByIdAsync(id, token);

            if(task == null)
            {
                return BadRequest();
            }

            var taskDto = await _taskService.UpdateAsync(task, newTask, token);
            return Ok(taskDto);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {

            _recorderService.LoggingMessage = "Recording time for GetAllAsync";

            _recorderService.Start();
                var tasks = await _taskService.GetAllAsync(token);
            _recorderService.Stop();



            _recorderService.LoggingMessage = "Recording time for Thread.Sleep()";

            _recorderService.Start();
                Thread.Sleep(5000);
            _recorderService.Stop();




            return Ok(tasks);
        }
    }
}
