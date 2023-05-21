using Demo.Api.Dtos;
using Demo.Api.InputModels;
using Demo.Data.Models;
using Microsoft.IdentityModel.Tokens;

namespace Demo.Api.Mapping
{
    public static class TaskMapping 
    {
        public static TaskDto ToDto(this Demo.Data.Models.Task task)
        {
           return new TaskDto
                  {
                      Id = task.Id,
                      Description = task.Description,
                      Status = task.Status,
                      Title = task.Title,
                      ProjectsNames = task.ProjectTasks.Select(p => p.Project.Name).AsEnumerable()
                  };
        }

        public static Demo.Data.Models.Task ToEntity(this TaskInputModel taskInputModel)
        {
            var taskEntity = new Demo.Data.Models.Task
            {
                Description = taskInputModel.Description,
                Status = taskInputModel.Status,
                Title = taskInputModel.Title,
            };

            return taskEntity;

        }
    }
}
