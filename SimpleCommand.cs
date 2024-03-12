using System.Windows.Input;

namespace MatchesGameWPF
{
    public class SimpleCommand : ICommand
    {
        public SimpleCommand(Func<bool> canExecute = null, Action execute = null)
        {
            CanExecuteDelegate = canExecute;
            ExecuteDelegate = execute;
        }
        public SimpleCommand(Action execute = null) : this(null, execute)
        {
            ExecuteDelegate = execute;
        }

        public Func<bool> CanExecuteDelegate { get; set; }
        public Action ExecuteDelegate { get; set; }

        public bool CanExecute(object parameter)
        {
            var canExecute = CanExecuteDelegate;
            return canExecute == null || canExecute();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            ExecuteDelegate?.Invoke();
        }
    }

    public class SimpleCommand<T> : ICommand
    {
        public SimpleCommand(Func<object, bool> canExecute = null, Action<object> execute = null)
        {
            CanExecuteDelegate = canExecute;
            ExecuteDelegate = execute;
        }
        public SimpleCommand(Action<object> execute = null) : this(null, execute)
        {
            ExecuteDelegate = execute;
        }

        public Func<object, bool> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegate { get; set; }

        public bool CanExecute(object parameter)
        {
            var canExecute = CanExecuteDelegate;
            return canExecute == null || canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            ExecuteDelegate?.Invoke(parameter);
        }
    }
}
