using Asana.Maui.ViewModel;

namespace Asana.Maui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private void AddNewClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ToDoDetails");
        }
         private void AddNewProjClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ProjectDetails");
        }

        private void EditClicked(object sender, EventArgs e)
        {
            var selectedId = (BindingContext as MainPageViewModel)?.SelectedToDoId ?? 0;
            Shell.Current.GoToAsync($"//ToDoDetails?toDoId={selectedId}");
        }

        private void EditProjClicked(object sender, EventArgs e)
        {
            var selectedId = (BindingContext as MainPageViewModel)?.SelectedProjectId ?? 0;
            Shell.Current.GoToAsync($"//ProjectDetails?ProjectId={selectedId}");
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as MainPageViewModel)?.DeleteToDo();
        }
        
        private void DeleteProjClicked(object sender, EventArgs e)
        {
            (BindingContext as MainPageViewModel)?.DeleteProject();
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            var viewModel = BindingContext as MainPageViewModel;
            viewModel?.RefreshProjectPage();

        }

        private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
        {

        }
       private void InLineDeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as MainPageViewModel)?.RefreshPage();
        }


    }

}