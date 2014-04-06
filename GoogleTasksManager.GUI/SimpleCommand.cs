using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GoogleTasksManager.GUI
{
    internal class SimpleCommand : ICommand
    {
        private Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;
        private Action<object> _execute;

        public SimpleCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public SimpleCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
