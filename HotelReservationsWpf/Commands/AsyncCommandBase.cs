using System.Windows.Input;

namespace HotelReservationsWpf.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        // Property to check if the command is executing
        private bool _isExecuting;
        public bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                if (_isExecuting != value)
                {
                    _isExecuting = value;
                    // Notify that the _isExecuting property has changed
                    OnCanExecuteChanged();
                }
            }
        }

        // Override the CanExecute method to check if the command is executing
        public override bool CanExecute(object? parameter)
            => !IsExecuting && base.CanExecute(parameter);

        // Override the Execute method to execute the command asynchronously
        public override async void Execute(object? parameter)
        {
            IsExecuting = true;

            try
            {
                // Execute the command asynchronously 
                await ExecuteAsync(parameter);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            IsExecuting = false;
        }

        // Abstract async method to execute the command
        public abstract Task ExecuteAsync(object? parameter);
    }
}
