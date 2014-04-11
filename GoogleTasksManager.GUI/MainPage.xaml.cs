using GoogleTasksManager.GUI.ViewModels;
using Microsoft.Phone.Controls;
using Tasks.Persistence;

namespace GoogleTasksManager.GUI
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new TaskListsViewModel();
        }
    }
}