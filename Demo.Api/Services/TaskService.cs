using Demo.Api.Dtos;
using Demo.Api.InputModels;
using Demo.Api.Mapping;
using Demo.Data.Data;
using Demo.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly DataContext _daContext;

        public TaskService(DataContext daContext)
        {
            _daContext = daContext;
        }

        public async Task<Demo.Data.Models.Task?> AddAsync(TaskInputModel newTask , CancellationToken token)
        {
            var task = newTask.ToEntity();

            //add projects to task
            foreach (var projectId in newTask.Projects)
            {
                task.ProjectTasks.Add(new ProjectTask { ProjectId = projectId });
            }

            //add to database 
            await _daContext.AddAsync(task, token);
            await _daContext.SaveChangesAsync(token);
            return task;
        }


        public async System.Threading.Tasks.Task DeleteAsync(Demo.Data.Models.Task task, CancellationToken token)
        {
            _daContext.Remove(task);
            await _daContext.SaveChangesAsync(token);
        }
        public async Task<IEnumerable<TaskDto>> GetAllAsync(CancellationToken token)
        {
            return await _daContext.Tasks
                                   .Include(t => t.ProjectTasks)
                                   .ThenInclude(p => p.Project)
                                   .Select(task => task.ToDto())
                                   .ToListAsync(token);
        }
        public async Task<Demo.Data.Models.Task?> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _daContext.Tasks.FindAsync(id, token);
        }
        public async Task<TaskDto?> GetDtoByIdAsync(Guid id, CancellationToken token)
        {
            var task = await _daContext.Tasks
                                             .Include(p => p.ProjectTasks)
                                             .ThenInclude(p => p.Project)
                                             .SingleOrDefaultAsync(t => t.Id == id ,token);
            return task?.ToDto();
        }
        public async Task<TaskDto> UpdateAsync(Demo.Data.Models.Task task, TaskInputModel taskInputModel, CancellationToken token)
        {
            task.Status = taskInputModel.Status;
            task.Title = taskInputModel.Title;
            task.Description = taskInputModel.Description;

            await _daContext.SaveChangesAsync(token);

            return task.ToDto();
        }



    }
}
