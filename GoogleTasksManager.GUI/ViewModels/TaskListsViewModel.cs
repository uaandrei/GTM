using GoogleTasksService;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Tasks.Model;
using Tasks.Persistence;

namespace GoogleTasksManager.GUI.ViewModels
{
    internal class TaskListsViewModel : BaseViewModel
    {
        public ObservableCollection<TaskList> TaskLists { get; set; }

        public ICommand DisplayTasksForTaskListCommand { get; set; }

        public ICommand SyncTaskListsCommand { get; set; }

        public TaskListsViewModel()
        {
            TaskLists = new ObservableCollection<TaskList>();
            SyncTaskListsCommand = new SimpleCommand(SyncTaskLists, CanSyncTaskLists);
            DisplayTasksForTaskListCommand = new SimpleCommand(DisplayTasksForTaskList, IsTaskListSelected);
            TaskContainer.GetAllTaskLists().ForEach(p => TaskLists.Add(p));
        }

        private bool CanSyncTaskLists(object arg)
        {
            return NetworkInterface.NetworkInterfaceType != NetworkInterfaceType.None;
        }

        private bool IsTaskListSelected(object param)
        {
            return param != null;
        }

        private async void DisplayTasksForTaskList(object param)
        {
            var taskList = (TaskList)param;
            App.RootFrame.Navigate(new Uri("/Views/TasksView.xaml?taskListId=" + taskList.Id, UriKind.Relative));
        }

        private async void SyncTaskLists(object obj)
        {
            ClearTaskLists();
            var taskLists = await GoogleTaskService.GetTaskLists();
            taskLists.ForEach(p => TaskLists.Add(p));
            TaskContainer.SyncTaskLists(taskLists);
        }

        private void ClearTaskLists()
        {
            TaskLists.Clear();
        }
    }
}
