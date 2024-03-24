using System.Windows.Input;

namespace HotelReservationsWpf.Commands
{
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
