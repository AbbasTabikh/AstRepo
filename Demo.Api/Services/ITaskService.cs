using Demo.Api.Dtos;
using Demo.Api.InputModels;

namespace Demo.Api.Services
{
    public interface ITaskService
    {
        Task<Demo.Data.Models.Task?> GetByIdAsync(Guid id, CancellationToken token);
        Task<TaskDto?> GetDtoByIdAsync(Guid id, CancellationToken token);
        Task<IEnumerable<TaskDto>> GetAllAsync(CancellationToken token);
        Task<Demo.Data.Models.Task?> AddAsync(TaskInputModel newTask, CancellationToken token);
        System.Threading.Tasks.Task DeleteAsync(Demo.Data.Models.Task task, CancellationToken token);
        Task<TaskDto> UpdateAsync(Demo.Data.Models.Task task, TaskInputModel taskInputModel, CancellationToken token);
    }
}
