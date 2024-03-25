using System.Windows.Input;

namespace HotelReservationsWpf.Commands
{
    public class RelayCommand<T> : ICommand
    {
        // delegate that takes a parameter of type T and returns a bool (Can be executed or not ?)
        private readonly Predicate<T?> _canExecute;
        // delegate that takes a parameter of type T executes the command
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute, Predicate<T?> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        // Event that is raised when the CanExecute method changes
        public event EventHandler? CanExecuteChanged;

        // Method to verify if the _canExecute is implemented or not
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute((T?)parameter);
        }

        // Mehod to execute the command
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
