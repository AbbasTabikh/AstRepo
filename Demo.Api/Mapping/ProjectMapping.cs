using Demo.Api.Dtos;
using Demo.Api.InputModels;
using Demo.Data.Models;


namespace Demo.Api.Mapping
{
    public static class ProjectMapping
    {
        public static Project ToEntity(this ProjectInputModel projectInputModel)
        {
            return new Project
            {
                Description = projectInputModel.Description,
                Image = projectInputModel.Image,
                Name = projectInputModel.Name
            };
        }

        public static ProjectDto ToDto(this Project project)
        {
            return new ProjectDto
            {
                Id = project.Id,
                Description = project.Description,
                Image = project.Image,
                Name = project.Name,
                Tasks = project.ProjectTasks.Select(pt => pt.Task.Description).ToList()
            };
        }
    }
}
