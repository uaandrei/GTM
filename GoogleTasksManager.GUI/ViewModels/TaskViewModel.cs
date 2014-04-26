using System.Windows.Input;
using Tasks.Model;
namespace GoogleTasksManager.GUI.ViewModels
{
    internal class TaskViewModel : BaseViewModel
    {
        private Task _task;
        private string _taskListId;

        public string Title
        {
            get
            {
                return _task.Title;
            }
            set
            {
                _task.Title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public string Description
        {
            get
            {
                return _task.Description;
            }
            set
            {
                _task.Description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public ICommand SaveTaskCommand { get; set; }

        #region ctor

        public TaskViewModel(string taskId, string taskListId)
        {
            _taskListId = taskListId;
            _task = TaskContainer.GetTask(taskId);
            Initialize();
        }

        public TaskViewModel(string taskListId)
        {
            _taskListId = taskListId;
            _task = new Task();
            Initialize();
        }

        private void Initialize()
        {
            SaveTaskCommand = new SimpleCommand(SaveTask);
        }

        #endregion

        private void SaveTask(object obj)
        {
            TaskContainer.SaveTask(_task, _taskListId);
            App.RootFrame.GoBack();
        }
    }
}