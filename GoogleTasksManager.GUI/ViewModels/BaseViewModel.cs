using GoogleTasksService;
using System.ComponentModel;
using Tasks.Persistence;

namespace GoogleTasksManager.GUI.ViewModels
{
    internal class BaseViewModel : INotifyPropertyChanged
    {
        protected TaskContainer TaskContainer;
        protected GoogleTaskService GoogleTaskService;

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
        {
            TaskContainer = new TaskContainer();
            GoogleTaskService = new GoogleTaskService();
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
