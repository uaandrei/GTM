using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Tasks.Model;

namespace GoogleTasksManager.GUI.ViewModels
{
    internal class TasksViewModel : BaseViewModel
    {
        private TaskList _taskList;

        public ObservableCollection<Task> Tasks { get; set; }
        public ICommand EditTaskCommand { get; set; }
        public ICommand NewTaskCommand { get; set; }

        public string TaskListName
        {
            get { return _taskList.Name; }
        }

        public TasksViewModel(string taskListId)
        {
            EditTaskCommand = new SimpleCommand(EditTask, CanEditTask);
            NewTaskCommand = new SimpleCommand(NewTask);
            Tasks = new ObservableCollection<Task>();
            TaskContainer.GetTasksForTaskList(taskListId).ForEach(p => Tasks.Add(p));
            _taskList = TaskContainer.GetTaskList(taskListId);
        }

        private void NewTask(object obj)
        {
            App.RootFrame.Navigate(new Uri(string.Format("/Views/TaskView.xaml?taskListId={0}", _taskList.GoogleId), UriKind.Relative));
        }

        private bool CanEditTask(object arg)
        {
            return arg is Task;
        }

        private void EditTask(object obj)
        {
            var task = (Task)obj;
            App.RootFrame.Navigate(new Uri(string.Format("/Views/TaskView.xaml?taskListId={0}&taskId=", _taskList.GoogleId, task.GoogleId), UriKind.Relative));
        }
    }
}
