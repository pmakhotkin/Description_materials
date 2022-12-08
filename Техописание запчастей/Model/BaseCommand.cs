

namespace Техописание_запчастей.Model
{
    public class BaseCommand : ICommand
    {
        private readonly Predicate<object?> _canExecute;
        private readonly Action<object?> _execute;
        public bool CanExecute(object? parameter)
        {
           return _canExecute.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute.Invoke(parameter);
        }
        public event EventHandler? CanExecuteChanged;

        public BaseCommand(Predicate<object?> canExecute, Action<object?> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }
    }
}
