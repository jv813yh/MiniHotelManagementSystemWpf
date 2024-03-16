using System.Windows.Input;

namespace HotelReservationsWpf.Commands
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T?> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute, Predicate<T?> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute((T?)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
