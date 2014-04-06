using GoogleTasksManager.GUI.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Tasks.Model;

namespace GoogleTasksManager.GUI.Views
{
    public partial class TasksView : PhoneApplicationPage
    {
        public TasksView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var tasks = (PhoneApplicationService.Current.State["tasks"] as List<Task>);
            var taskListName = NavigationContext.QueryString["taskListName"];
            DataContext = new TasksViewModel(tasks, taskListName);
        }
    }
}