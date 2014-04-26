using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Tasks.Model;
using GoogleTasksManager.GUI.ViewModels;

namespace GoogleTasksManager.GUI.Views
{
    public partial class TaskView : PhoneApplicationPage
    {
        public TaskView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            TaskViewModel context;

            var taskListId = NavigationContext.QueryString["taskListId"];
            if (NavigationContext.QueryString.ContainsKey("taskId"))
            {
                var taskId = NavigationContext.QueryString["taskId"];
                context = new TaskViewModel(taskId, taskListId);
            }
            else
            {
                context = new TaskViewModel(taskListId);
            }

            DataContext = context;
        }
    }
}