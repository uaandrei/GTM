using System.Windows.Input;
using Tasks.Model;
namespace GoogleTasksManager.GUI.ViewModels
{
    internal class TaskViewModel : BaseViewModel
    {
        private Task _task;

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

        public TaskViewModel(string taskId)
        {
            _task = TaskContainer.GetTask(taskId);
            SaveTaskCommand = new SimpleCommand(SaveTask, CanSaveTask);
        }

        private void SaveTask(object obj)
        {
            TaskContainer.SaveTask(_task);
            App.RootFrame.GoBack();
        }

        private bool CanSaveTask(object arg)
        {
            return !string.IsNullOrWhiteSpace(Title);
        }
    }
}