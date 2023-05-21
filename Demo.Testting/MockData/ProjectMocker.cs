using Demo.Data.Models;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Testing.MockData
{
    public class ProjectMocker
    {
        private const int  NumberOfProjects = 25;
        private static List<Project> _projects;


        public static List<Project> GetProjects()
        {
            _projects = new List<Project>();

            for (int i = 0; i < NumberOfProjects; i++)
            {
                _projects.Add(new Project
                {
                    Id = Guid.NewGuid(),
                    Description = $"description-{i}",
                    Name = $"Project-{i}",
                    Image = null
                });
            }

            return _projects;
        }

        public static Project GetAny()
        {
            int max = _projects.Count;

            int random = new Random().Next();
            return _projects?.ElementAt(random % max);
        }
    }
}
