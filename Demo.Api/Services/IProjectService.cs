using Demo.Api.Dtos;
using Demo.Api.InputModels;
using Demo.Data.Models;

namespace Demo.Api.Services
{
    public interface IProjectService
    {
        Task<Project?> GetByIdAsync(Guid id, CancellationToken token);
        Task<ProjectDto?> GetDtoByIdAsync(Guid id, CancellationToken token);
        Task<IEnumerable<ProjectDto>> GetAllAsync(CancellationToken token);
        Task<Project> AddAsync(ProjectInputModel newProject, CancellationToken token);
        System.Threading.Tasks.Task DeleteAsync(Project project, CancellationToken token);
        Task<ProjectDto> UpdateAsync(Project project, ProjectInputModel projectInputModel, CancellationToken token);
        public Task<ProjectDto?> AddTasksToProjectAsync(Project project, IEnumerable<Guid> taskIds, CancellationToken token);
        public Task<ProjectDto> RemoveTasksFromProjectAsync(Project project, IEnumerable<Guid> taskIds, CancellationToken token);
    }
}
