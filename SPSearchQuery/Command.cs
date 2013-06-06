using System;

namespace MunirHusseini.SPSearchPad
{
    public class Command : Command<object>
    {
        public Command(Action execute)
            : this(execute, null)
        {
        }

        public Command(Action execute, Func<bool> canExecute)
            : base(p => execute(), canExecute == null ? null : (Func<object, bool>)(p => canExecute()))
        {
        }
    }

    public class Command<T> : System.Windows.Input.ICommand
    {
        private readonly Action<T> _execute;

        private readonly Func<T, bool> _canExecute;

        public Command(Action<T> execute)
            : this(execute, null)
        {
        }

        public Command(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            if (_execute == null)
            {
                return;
            }

            var o = parameter is T ? (T)parameter : default(T);
            _execute(o);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                var o = parameter is T ? (T)parameter : default(T);
                return _canExecute(o);
            }

            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null) CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}