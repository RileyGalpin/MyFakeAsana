using System;
using Asana.Library.Services;

//17 branch
namespace Asana.Maui.ViewModel
{

    public class MainPageViewModel
    {
        ToDoServiceProxy _toDoSvc; 

        public MainPageViewModel()
        {
            _toDoSvc = ToDoServiceProxy.Current;
        }
    }

}