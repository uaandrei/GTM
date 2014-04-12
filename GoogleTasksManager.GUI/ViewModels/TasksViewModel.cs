using System.Collections.ObjectModel;
using Tasks.Model;

namespace GoogleTasksManager.GUI.ViewModels
{
    internal class TasksViewModel : BaseViewModel
    {
        private string _taskListName;
        private TaskList _taskList;

        public ObservableCollection<Task> Tasks { get; set; }

        public string TaskListName
        {
            get { return _taskList.Name; }
        }

        public TasksViewModel(string taskListId)
        {
            Tasks = new ObservableCollection<Task>();
            TaskContainer.GetTasksForTaskList(taskListId).ForEach(p => Tasks.Add(p));
            _taskList = TaskContainer.GetTaskList(taskListId);
        }
    }
}
