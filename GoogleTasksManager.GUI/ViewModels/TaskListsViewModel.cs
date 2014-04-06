using GoogleTasksService;
using Microsoft.Phone.Shell;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Navigation;
using Tasks.Model;

namespace GoogleTasksManager.GUI.ViewModels
{
    internal class TaskListsViewModel : BaseViewModel
    {
        private GoogleTaskService _googleTaskService;

        public ObservableCollection<TaskList> TaskLists { get; set; }

        public ICommand DisplayTasksForTaskListCommand { get; set; }

        public ICommand UpdateTaskListsCommand { get; set; }

        public TaskListsViewModel()
        {
            _googleTaskService = new GoogleTaskService();
            TaskLists = new ObservableCollection<TaskList>();
            UpdateTaskListsCommand = new SimpleCommand(FetchAllTaskLists);
            DisplayTasksForTaskListCommand = new SimpleCommand(FetchAndDisplayAllTasksForTaskList, CanFetchAllTasksForTaskList);
        }

        private bool CanFetchAllTasksForTaskList(object param)
        {
            return param != null;
        }

        private async void FetchAndDisplayAllTasksForTaskList(object param)
        {
            var taskList = (TaskList)param;
            var tasks = await _googleTaskService.GetTasksForTaskList(taskList);
            PhoneApplicationService.Current.State["tasks"] = tasks;
            App.RootFrame.Navigate(new Uri("/Views/TasksView.xaml?taskListName=" + taskList.Name, UriKind.Relative));
        }

        private async void FetchAllTaskLists(object obj)
        {
            ClearTasks();
            var taskLists = await _googleTaskService.GetTaskLists();
            taskLists.ForEach(p => TaskLists.Add(p));
        }

        private void ClearTasks()
        {
            TaskLists.Clear();
        }
    }
}
