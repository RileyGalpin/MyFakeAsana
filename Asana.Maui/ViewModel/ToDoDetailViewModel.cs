using Asana.Library.Models;
using Asana.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Asana.Maui.ViewModel
{
    public class ToDoDetailViewModel
    {
         private ProjectServiceProxy _projectSvc;
        public ToDoDetailViewModel()
        {
            Model = new ToDo();
            _projectSvc = ProjectServiceProxy.Current;
            DeleteCommand = new Command(DoDelete);
        }

        public ToDoDetailViewModel(int id)
        {
            Model = ToDoServiceProxy.Current.GetById(id) ?? new ToDo();
             _projectSvc = ProjectServiceProxy.Current;
            DeleteCommand = new Command(DoDelete);
        }

        public ToDoDetailViewModel(ToDo? model)
        {
            Model = model ?? new ToDo();
             _projectSvc = ProjectServiceProxy.Current;
            DeleteCommand = new Command(DoDelete);
        }

        public void DoDelete()
        {
            foreach (var project in _projectSvc.Projects)
            {
                if (project.ToDos != null && project.ToDos.Contains(Model))
                {
                    project.ToDos.Remove(Model);
                }
            }
            ToDoServiceProxy.Current.DeleteToDo(Model);

            
        }

        public ToDo? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }

        public List<int> Priorities
        {
            get
            {
                return new List<int> { 0, 1, 2, 3, 4 };
            }
        }

        public int SelectedPriority
        {
            get
            {
                return Model?.Priority ?? 4;
            }
            set
            {
                if (Model != null && Model.Priority != value)
                {
                    Model.Priority = value;
                }
            }
        }

        public void AddOrUpdateToDo()
        {
            if (SelectedProject != null)
            {
                _projectSvc.GetById(SelectedProjectId).AddOrUpdate(Model);
            }
            ToDoServiceProxy.Current.AddOrUpdate(Model);
        }

        //This is option 1 to fix the UX issue with Priority
        public string PriorityDisplay
        {
            set
            {
                if (Model == null)
                {
                    return;
                }

                if (!int.TryParse(value, out int p))
                {
                    Model.Priority = -9999;
                }
                else
                {
                    Model.Priority = p;
                }
            }

            get
            {
                return Model?.Priority?.ToString() ?? string.Empty;
            }
        }

private ObservableCollection<Project> _projects;
public ObservableCollection<Project> Projects
{
    get
    {
        if (_projects == null)
        {
            var projects = _projectSvc.Projects ?? new List<Project>();
            _projects = new ObservableCollection<Project>(projects);
        }
                foreach (var project in _projects)
                {
                    if (project.Id == -1)
                    {
                        _projects.Remove(project);
                        break; 
                    }
        }
        return _projects;
    }
    set
    {
        _projects = value;
        //RefreshProjectPage();
    }
}



        // public double CompletedPercent()

        public Project SelectedProject { get; set; }
        public int SelectedProjectId => SelectedProject?.Id ?? -1;


    }
}