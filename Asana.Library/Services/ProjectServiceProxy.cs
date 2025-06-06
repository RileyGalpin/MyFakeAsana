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
        public List<Project> projects;

        private ProjectServiceProxy()
        {
            projects = new List<Project>();
        }

        private static ProjectServiceProxy? instance;

        private int nextKey
        {
            get
            {
                if (projects.Any())
                {
                    return projects.Select(p => p.Id).Max() + 1;
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
                projects.Add(project);
            }
        }

        public void DisplayProjects()
        {
            projects.ForEach(Console.WriteLine);
        }

        public Project? GetById(int id)
        {
            return projects.FirstOrDefault(p => p.Id == id);
        }

        public void DeleteProject(Project? project)
        {
            if (project == null)
            {
                return;
            }
            projects.Remove(project);
        }
    }    
    }
