using Asana.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.Services
{

    public class ProjectServiceProxy
    {

        private List<Project> _projectList;

       private List<ToDo> _projectToDoList;
       

        public List<Project> Projects


        {
            get
            {
                return _projectList.Take(100).ToList();
            }
            private set
            {
                if (value != _projectList)
                {
                    _projectList = value;
                }
            }
        }

        private ProjectServiceProxy()
        {
            Projects = new List<Project>
            {
                new Project{Id = 1, Name = "Project 1", Description = "My Project 1", ToDos = new List<ToDo>()},
                new Project{Id = 2, Name = "Project 2", Description = "My Project 2", ToDos = new List<ToDo>()},
                new Project{Id = 3, Name = "Project 3", Description = "My Project 3", ToDos = new List<ToDo>()}
            };
        }

        private static ProjectServiceProxy? instance;

        private int nextKey
        {
            get
            {
                if (_projectList.Any())
                {
                    return _projectList.Select(p => p.Id).Max() + 1;
                }
                return 1;
            }
        }

        public static ProjectServiceProxy Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProjectServiceProxy();
                }

                return instance;
            }
        }


        public void AddOrUpdate(Project? project)
        {
            if (project != null && project.Id == 0)
            {
                project.Id = nextKey;
                _projectList.Add(project);
            }
        }

        public void DisplayProjects()
        {
            _projectList.ForEach(Console.WriteLine);
        }

        public Project? GetById(int id)
        {
            return _projectList.FirstOrDefault(p => p.Id == id);
        }

        public void DeleteProject(Project? project)
        {
            if (project == null)
            {
                return;
            }
            _projectList.Remove(project);
        }
        public List<ToDo> GetToDosForProject(int projectId)
            {
                var project = GetById(projectId);
                return project?.ToDos ?? new List<ToDo>();
            }
       
        public void AddToDoToProject(int projectId, ToDo todo)
        {
            var project = GetById(projectId);
            project?.AddOrUpdate(todo);
        }
    }    
    }
