using Asana.Library.Models;
using Asana.Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Maui.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ToDoServiceProxy _toDoSvc;
        private ProjectServiceProxy _projectSvc;
        private Project _Model;
         

        public MainPageViewModel()
        {
            _toDoSvc = ToDoServiceProxy.Current;
            _projectSvc = ProjectServiceProxy.Current;
            _Model = new Project();

        }

        public ToDoDetailViewModel SelectedToDo { get; set; }

        public ObservableCollection<ToDoDetailViewModel> ToDos
        {
            get
            {

                var selectedProject = _projectSvc.GetById(SelectedProjectId);
                if (selectedProject != null && selectedProject.ToDos != null)
                {
                    var toDos = selectedProject.ToDos.Select(t => new ToDoDetailViewModel(t));
                    if (selectedProject.Id == -1)
                    {
                        foreach (var project in _projectSvc.Projects)
                        {
                            if (project.ToDos != null)
                            {
                                toDos = toDos.Concat(project.ToDos.Select(t => new ToDoDetailViewModel(t)));
                            }
                        }
                            toDos = toDos.Concat(_toDoSvc.ToDos .Select(t => new ToDoDetailViewModel(t)));
                        //loop through each project id and display all todos in the projects (handle completed vs not completed todos)
                    }
                    if (!IsShowCompleted)
                    {
                        toDos = toDos.Where(t => !t?.Model?.IsCompleted ?? false);
                    }
                    //NotifyPropertyChanged(nameof(ToDos));
                    return new ObservableCollection<ToDoDetailViewModel>(toDos);
                }
                else{   
                    var toDos = _toDoSvc.ToDos
                        .Select(t => new ToDoDetailViewModel(t));
                        
                    if (!IsShowCompleted)
                    {
                        toDos = toDos.Where(t => !t?.Model?.IsCompleted ?? false);
                    }    
                   // NotifyPropertyChanged(nameof(ToDos));
                    return new ObservableCollection<ToDoDetailViewModel>(toDos);
                }
    
                
            }
        }

        public int SelectedToDoId => SelectedToDo?.Model?.Id ?? 0;

        private bool isShowCompleted;
        public bool IsShowCompleted
        {
            get
            {
                return isShowCompleted;
            }

            set
            {
                if (isShowCompleted != value)
                {
                    isShowCompleted = value;
                    NotifyPropertyChanged(nameof(ToDos));
                }
            }
        }

        public void DeleteToDo()
        {
            if (SelectedToDo == null)
            {
                return;
            }
            if (SelectedProject != null)
                {
                    SelectedProject.ToDos.Remove(SelectedToDo.Model);
                }
          
            ToDoServiceProxy.Current.DeleteToDo(SelectedToDo.Model);
            NotifyPropertyChanged(nameof(ToDos));
        }
        
        public void DeleteProject()
        {
            if (SelectedProject == null)
            {
                return;
            }
            if (SelectedProject != null && SelectedProject.ToDos != null)
                {
                    foreach (var todo in SelectedProject.ToDos)
                    {
                        ToDoServiceProxy.Current.DeleteToDo(todo);
                    }         
                }
          
            ProjectServiceProxy.Current.DeleteProject(SelectedProject);
            NotifyPropertyChanged(nameof(Projects));
        }

        public void RefreshPage()
        {
            NotifyPropertyChanged(nameof(ToDos));
            //NotifyPropertyChanged(nameof(Projects));
            // NotifyPropertyChanged(nameof(SelectedProject));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        return _projects;
    }
    set
    {
        _projects = value;
        NotifyPropertyChanged();
    }
}



    public Project SelectedProject { get; set; }


    public int SelectedProjectId => SelectedProject?.Id ?? -999;

 

        public void RefreshProjectPage()
        {
            _projects = null; // Clear cache
            NotifyPropertyChanged(nameof(Projects));
        }

    }
}
