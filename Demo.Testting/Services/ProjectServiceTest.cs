using Demo.Api.Services;
using Demo.Data.Data;
using Demo.Data.Models;
using Demo.Testing.MockData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Testing.Services
{
    public class ProjectServiceTest : IDisposable
    {
        private readonly DataContext _dbContext;
        private IProjectService _projectService;


        public ProjectServiceTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _dbContext = new DataContext(options);
            _dbContext.Database.EnsureCreated();
        }


        [Fact]
        public async System.Threading.Tasks.Task GetByIdAsyncTest_ReturnsRecord()
        {
            Seed();

            var project = ProjectMocker.GetAny();

            _projectService =  new ProjectService(_dbContext);

            var resultObject = await _projectService.GetByIdAsync(project.Id, CancellationToken.None);

            Assert.NotNull(resultObject);
            Assert.Equal(project.Id, resultObject.Id);

        }



        [Fact]
        public void Dispose()
        {
            //to ensure fresh start for every test
            _dbContext.Database.EnsureDeleted();
        }


        public void Seed()
        {
            var projects = ProjectMocker.GetProjects().ToList();

            _dbContext.AddRange(projects);
            _dbContext.SaveChanges();
        }
    }
}
