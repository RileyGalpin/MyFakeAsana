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
    public class ProjectDetailViewModel
    {
        private ProjectServiceProxy _projectSvc;
        public ProjectDetailViewModel()
        {
            Model = new Project();
            _projectSvc = ProjectServiceProxy.Current;
            DeleteCommand = new Command(DoDelete);
        }

        public ProjectDetailViewModel(int id)
        {
            Model = ProjectServiceProxy.Current.GetById(id) ?? new Project();
             _projectSvc = ProjectServiceProxy.Current;
            DeleteCommand = new Command(DoDelete);
        }

        public ProjectDetailViewModel(Project? model)
        {
            Model = model ?? new Project();
             _projectSvc = ProjectServiceProxy.Current;
            DeleteCommand = new Command(DoDelete);
        }

        public void DoDelete()
        {
            _projectSvc.DeleteProject(Model);
        }

        public Project? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }

        public void AddOrUpdateProject()
        {
         
            _projectSvc.AddOrUpdate(Model);
        }

        //This is option 1 to fix the UX issue with Priority

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
        //RefreshProjectPage();
    }
}



        // public double CompletedPercent()

        public Project SelectedProject { get; set; }
        public int SelectedProjectId => SelectedProject?.Id ?? -1;


    }
}