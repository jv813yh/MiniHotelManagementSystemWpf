using System.Windows.Input;

namespace HotelReservationsWpf.Commands
{
    // Base abstract class for implementing commands in the application
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual void OnCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public virtual bool CanExecute(object? parameter)
            => true;

        public abstract void Execute(object? parameter);
    }
}
