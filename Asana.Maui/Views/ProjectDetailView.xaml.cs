using Asana.Library.Models;
using Asana.Maui.ViewModel;

namespace Asana.Maui.Views;

[QueryProperty(nameof(ProjectId), "ProjectId")]
public partial class ProjectDetailView : ContentPage
{
	public ProjectDetailView()
	{
		InitializeComponent();
        
	}
    public int ProjectId { get; set; }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void OkClicked(object sender, EventArgs e)
    {
		 var viewModel = BindingContext as ProjectDetailViewModel;
    	viewModel?.AddOrUpdateProject();
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectDetailViewModel(ProjectId);
    }
}