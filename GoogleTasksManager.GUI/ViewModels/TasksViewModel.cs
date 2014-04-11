using Microsoft.Phone.Net.NetworkInformation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Tasks.Model;

namespace GoogleTasksManager.GUI.ViewModels
{
    internal class TasksViewModel : BaseViewModel
    {
        private string _taskListName;
        private TaskList _taskList;

        public ObservableCollection<Task> Tasks { get; set; }

        public ICommand SyncTasksCommand { get; set; }

        public string TaskListName
        {
            get { return _taskList.Name; }
        }

        public TasksViewModel(string taskListId)
        {
            Tasks = new ObservableCollection<Task>();

            SyncTasksCommand = new SimpleCommand(SyncTasks, CanSyncTasks);

            TaskContainer.GetTasksForTaskList(taskListId).ForEach(p => Tasks.Add(p));
            _taskList = TaskContainer.GetTaskList(taskListId);
        }

        private bool CanSyncTasks(object arg)
        {
            return NetworkInterface.NetworkInterfaceType != NetworkInterfaceType.None;
        }

        private async void SyncTasks(object obj)
        {
            ClearTasks();
            var tasks = await GoogleTaskService.GetTasksForTaskList(_taskList);
            tasks.ForEach(p => Tasks.Add(p));
            TaskContainer.SyncTasksWithTaskList(tasks, _taskList.Id);
        }

        private void ClearTasks()
        {
            Tasks.Clear();
        }
    }
}
