using Demo.Api.Dtos;
using Demo.Api.InputModels;
using Demo.Api.Mapping;
using Demo.Data.Data;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Api.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DataContext _dataContext;

        public ProjectService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Project?> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _dataContext.Projects.FindAsync(id, token);
        }

        public async Task<ProjectDto?> GetDtoByIdAsync(Guid id, CancellationToken token)
        {
            var project = await _dataContext.Projects
                                             .Include(p => p.ProjectTasks)
                                             .ThenInclude(t => t.Task)
                                             .SingleOrDefaultAsync(p => p.Id == id, token);
            return project?.ToDto();

        }
        public async Task<IEnumerable<ProjectDto>> GetAllAsync(CancellationToken token)
        { 
                var projects = await _dataContext.Projects
                                     .Include(p => p.ProjectTasks)
                                     .ThenInclude(t => t.Task).ToListAsync();

                return projects.Select(p => p.ToDto()).ToList();
        }
        public async Task<Project> AddAsync(ProjectInputModel newProject, CancellationToken token)
        {
            Project project = newProject.ToEntity();
            await _dataContext.AddAsync(project, token);
            await _dataContext.SaveChangesAsync(token);
            return project;
        }
        public async System.Threading.Tasks.Task DeleteAsync(Project project, CancellationToken token)
        {
            _dataContext.Projects.Remove(project);
            await _dataContext.SaveChangesAsync(token);

        }
        public async Task<ProjectDto> UpdateAsync(Project project, ProjectInputModel projectInputModel, CancellationToken token)
        {
            project.Name = projectInputModel.Name;
            project.Description = projectInputModel.Description;
            project.Image = projectInputModel.Image;

            _dataContext.Projects.Update(project);

            await _dataContext.SaveChangesAsync(token);

            return project.ToDto();
        }



        // i added them directly to the project
        public async Task<ProjectDto?> AddTasksToProjectAsync(Project project, IEnumerable<Guid> taskIds, CancellationToken token)
        {
            var tasksToAdd = await _dataContext.Tasks.Where(t => taskIds.Contains(t.Id)).ToListAsync(cancellationToken: token);

            //if a task id doesn't exist
            if(tasksToAdd.Count != taskIds.Count())
            {
                return null;
            }

            //add .Except(project.ProjectTask) later
            var projectTasksToAdd = tasksToAdd.Select(t => new ProjectTask { Project = project, Task = t });

            project.ProjectTasks.AddRange(projectTasksToAdd);
            await _dataContext.SaveChangesAsync(token);
            return project.ToDto();
        }

        // i removed them from the ProjectTasks table
        public async Task<ProjectDto> RemoveTasksFromProjectAsync(Project project, IEnumerable<Guid> taskIds, CancellationToken token)
        {
            var projectTasksToRemove = _dataContext.ProjectTasks.Where(pt => taskIds.Contains(pt.TaskId) && pt.ProjectId == project.Id);
            _dataContext.ProjectTasks.RemoveRange(projectTasksToRemove);
            await _dataContext.SaveChangesAsync(token);
            return project.ToDto();
        }

    }
}
