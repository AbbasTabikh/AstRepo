using Castle.Core.Logging;
using Demo.Api.Controllers;
using Demo.Api.Dtos;
using Demo.Api.InputModels;
using Demo.Api.Mapping;
using Demo.Api.Models;
using Demo.Api.Services;
using Demo.Data.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Testing.ControllersTesting
{

    public class ProjectsControllerTest
    {
        private IProjectService _projectService = A.Fake<IProjectService>();
        private ProjectsController _projectsController;

        [Fact]
        public async System.Threading.Tasks.Task Get_Projects_Returns_Ok()
        {
            var projectsList = A.CollectionOfDummy<Project>(30).Select(p => p.ToDto());

            A.CallTo(() => _projectService.GetAllAsync(new CancellationToken())).Returns(System.Threading.Tasks.Task.FromResult(projectsList));


            var fakeLogger = A.Fake<ILogger<ProjectsController>>();

            _projectsController = new ProjectsController(_projectService, fakeLogger);
            var res = await _projectsController.Get(new CancellationToken());
            var resobject = res as OkObjectResult;
            Assert.Equal((int) HttpStatusCode.OK, resobject?.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task  Get_By_Id_Returns_Ok()
        {
            Guid projectId = Guid.NewGuid();

            var expectedProject = new Project { Id = projectId, Name = "Test Project" }.ToDto();


            A.CallTo(() => _projectService.GetDtoByIdAsync(projectId, new CancellationToken())).Returns(expectedProject);
            var fakeLogger = A.Fake<ILogger<ProjectsController>>();

            _projectsController = new ProjectsController(_projectService, fakeLogger);

            var result = await _projectsController.Get(projectId, new CancellationToken());

            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            var project = okResult?.Value as ProjectDto;

            Assert.NotNull(project);
            Assert.Equal(projectId, project.Id);

        }

        [Fact]
        public async System.Threading.Tasks.Task Get_By_Id_Returns_NotFound()
        {
            Guid nonExisitingprojectId = Guid.NewGuid();

            _ = A.CallTo(() => _projectService.GetDtoByIdAsync(nonExisitingprojectId, new CancellationToken()))
                                              .Returns<ProjectDto?>(null);

            var fakeLogger = A.Fake<ILogger<ProjectsController>>();

            _projectsController = new ProjectsController(_projectService , fakeLogger);

            var result = await _projectsController.Get(nonExisitingprojectId, new CancellationToken());

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async System.Threading.Tasks.Task Add_Project_Returns_Ok()
        {
            ProjectInputModel projectInputModel = new ProjectInputModel
            {
                Description = "description",
                Image = null,
                Name = "Test project"
            };

            Project project =  new Project
            {
                Id = Guid.NewGuid(),
                Description = "description",
                Image = null,
                Name = "Test project",
            };

            
            A.CallTo(() => _projectService.AddAsync(projectInputModel , new CancellationToken())).Returns(System.Threading.Tasks.Task.FromResult(project));

            var fakeLogger = A.Fake<ILogger<ProjectsController>>();

            _projectsController = new ProjectsController(_projectService, fakeLogger);

            var resultOk = await _projectsController.Add(projectInputModel, new CancellationToken());

            Assert.IsType<OkObjectResult>(resultOk);

            var resultObject = ((OkObjectResult) resultOk).Value as ProjectDto;

            Assert.NotNull(resultObject);
            Assert.Equal(resultObject.Id , project.Id);
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_Project_Returns_No_Content()
        {
            Project project = new Project
            {
                Id = Guid.NewGuid(),
                Image = null,
                Description = "description",
                Name = "Name"
            };

            A.CallTo(() => _projectService.GetByIdAsync(project.Id, CancellationToken.None)).Returns(project);

            var fakeLogger = A.Fake<ILogger<ProjectsController>>();

            _projectsController = new ProjectsController(_projectService, fakeLogger);

            var result = await _projectsController.Delete(project.Id, CancellationToken.None);

            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async System.Threading.Tasks.Task Delete_Project_Returns_NotFound()
        {
            var projectId = Guid.NewGuid();

            A.CallTo(() => _projectService.GetByIdAsync(projectId, CancellationToken.None)).Returns<Project?>(null);

            var fakeLogger = A.Fake<ILogger<ProjectsController>>();

            _projectsController = new ProjectsController(_projectService, fakeLogger);

            var result = await _projectsController.Delete(projectId, CancellationToken.None);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Add_Tasks_To_Project_Returns_Ok()
        {
            var project = new Project
            {
                Description = "description",
                Id = Guid.NewGuid(),
                Image = null,
                Name = "Name"
            };

            IEnumerable<Guid> guids = A.CollectionOfDummy<Guid>(5);

            A.CallTo(() => _projectService.GetByIdAsync(project.Id, CancellationToken.None)).Returns<Project?>(project);
            A.CallTo(() => _projectService.AddTasksToProjectAsync(project, guids, CancellationToken.None)).Returns(project.ToDto());

            var fakeLogger = A.Fake<ILogger<ProjectsController>>();

            _projectsController = new ProjectsController(_projectService, fakeLogger);

            var result = await _projectsController.AddTasks(project.Id , guids, CancellationToken.None);

            Assert.IsType<OkObjectResult>(result);

            var resultObject = (result as OkObjectResult)?.Value as ProjectDto;

            Assert.NotNull(resultObject);
            Assert.Equal(project.Id, resultObject.Id);
        }


        [Fact]
        public async System.Threading.Tasks.Task Add_Tasks_To_Project_Returns_NotFound()
        {
            Guid projectId = Guid.NewGuid();
            IEnumerable<Guid> guids = A.CollectionOfDummy<Guid>(5);

            A.CallTo(() => _projectService.GetByIdAsync(projectId, CancellationToken.None)).Returns<Project?>(null);

            var fakeLogger = A.Fake<ILogger<ProjectsController>>();

            _projectsController = new ProjectsController(_projectService, fakeLogger);

            var result = await _projectsController.AddTasks(projectId, guids, CancellationToken.None);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async System.Threading.Tasks.Task Add_Tasks_To_Project_Returns_BadRequest()
        {
            var project = new Project
            {
                Description = "description",
                Id = Guid.NewGuid(),
                Image = null,
                Name = "Name"
            };

            IEnumerable<Guid> guids = A.CollectionOfDummy<Guid>(5);

            A.CallTo(() => _projectService.GetByIdAsync(project.Id, CancellationToken.None)).Returns<Project?>(project);
            A.CallTo(() => _projectService.AddTasksToProjectAsync(project, guids, CancellationToken.None)).Returns<ProjectDto?>(null);

            var fakeLogger = A.Fake<ILogger<ProjectsController>>();

            _projectsController = new ProjectsController(_projectService, fakeLogger);

            var result = await _projectsController.AddTasks(project.Id, guids, CancellationToken.None);

            Assert.IsType<BadRequestResult>(result);
        }


        [Fact]
        public async System.Threading.Tasks.Task Remove_Tasks_From_Project_Returns_Ok()
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Description = "description",
                Image = null,
                Name = "Name"
            };

            IEnumerable<Guid> guids = A.CollectionOfDummy<Guid>(5);

            A.CallTo(() => _projectService.GetByIdAsync(project.Id, CancellationToken.None)).Returns<Project?>(project);
            A.CallTo(() => _projectService.RemoveTasksFromProjectAsync(project, guids, CancellationToken.None)).Returns(project.ToDto());

            var fakeLogger = A.Fake<ILogger<ProjectsController>>();

            _projectsController = new ProjectsController(_projectService, fakeLogger);

            var result = await _projectsController.RemoveTasks(project.Id, guids, CancellationToken.None);

            Assert.IsType<OkObjectResult>(result);

            var resultObject = (result as OkObjectResult)?.Value as ProjectDto;

            Assert.NotNull(resultObject);
            Assert.Equal(project.Id, resultObject.Id);
        }

        [Fact]
        public async System.Threading.Tasks.Task Remove_Tasks_From_Project_Returns_NotFound()
        {
            var projectId = Guid.NewGuid();
            IEnumerable<Guid> guids = A.CollectionOfDummy<Guid>(5);

            A.CallTo(() => _projectService.GetByIdAsync(projectId, CancellationToken.None)).Returns<Project?>(null);

            var fakeLogger = A.Fake<ILogger<ProjectsController>>();

            _projectsController = new ProjectsController(_projectService, fakeLogger);

            var result = await _projectsController.RemoveTasks(projectId, guids, CancellationToken.None);

            Assert.IsType<NotFoundResult>(result);
        }


    }
}
