using Microsoft.Phone.Net.NetworkInformation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Tasks.Model;
using Tasks.Persistence;
using GoogleTasksManager.Synchronization;

namespace GoogleTasksManager.GUI.ViewModels
{
    internal class TaskListsViewModel : BaseViewModel
    {
        public ObservableCollection<TaskList> TaskLists { get; set; }

        public ICommand DisplayTasksForTaskListCommand { get; set; }
        public ICommand SyncWithGoogleCommand { get; set; }

        public TaskListsViewModel()
        {
            TaskLists = new ObservableCollection<TaskList>();
            SyncWithGoogleCommand = new SimpleCommand(SyncWithGoogle, CanSyncWithGoogle);
            DisplayTasksForTaskListCommand = new SimpleCommand(DisplayTasksForTaskList, IsTaskListSelected);
            RefreshTaskLists();
        }

        private async void SyncWithGoogle(object obj)
        {
            ClearTaskLists();
            await TaskContainer.SyncWithGoogle();
            RefreshTaskLists();
        }

        private bool CanSyncWithGoogle(object arg)
        {
            return NetworkInterface.NetworkInterfaceType != NetworkInterfaceType.None;
        }

        private bool IsTaskListSelected(object param)
        {
            return param != null;
        }

        private void DisplayTasksForTaskList(object param)
        {
            var taskList = (TaskList)param;
            App.RootFrame.Navigate(new Uri("/Views/TasksView.xaml?taskListId=" + taskList.GoogleId, UriKind.Relative));
        }

        private void ClearTaskLists()
        {
            TaskLists.Clear();
        }

        private void RefreshTaskLists()
        {
            TaskContainer.GetAllTaskLists().ForEach(p => TaskLists.Add(p));
        }
    }
}
