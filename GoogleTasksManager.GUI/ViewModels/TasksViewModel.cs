using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tasks.Model;

namespace GoogleTasksManager.GUI.ViewModels
{
    internal class TasksViewModel : BaseViewModel
    {
        public ObservableCollection<Task> Tasks { get; set; }

        private string _taskListName;

        public string TaskListName
        {
            get { return _taskListName; }
            set
            {
                _taskListName = value;
                NotifyPropertyChanged("TaskListName");
            }
        }

        public TasksViewModel(List<Task> tasks, string taskListName)
        {
            Tasks = new ObservableCollection<Task>();
            tasks.ForEach(p => Tasks.Add(p));
            TaskListName = taskListName;
        }
    }
}
