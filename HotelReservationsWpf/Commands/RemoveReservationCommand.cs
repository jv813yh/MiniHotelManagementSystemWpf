using HotelReservationsWpf.Stores;
using HotelReservationsWpf.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace HotelReservationsWpf.Commands
{
    // Class for removing reservations from the hotel store using AsyncCommand and displaying a message box
    public class RemoveReservationCommand : AsyncCommandBase, IDisposable
    {
        private readonly HotelStore _hotelStore;
        private readonly OverviewViewModel _overviewViewModel;

        public RemoveReservationCommand(HotelStore hotelStore, OverviewViewModel overviewViewModel)
        {
            _hotelStore = hotelStore;
            _overviewViewModel = overviewViewModel;

            // Subscribe to the PropertyChanged event of the overview view model
            _overviewViewModel.PropertyChanged += OnViewModelChanged;
        }

        // Method that is called when the properties from view model changes
        // to check if the command can be executed
        private void OnViewModelChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_overviewViewModel.RoomNumberString)
                || e.PropertyName == nameof(_overviewViewModel.GuestName))
            {
                // Call the RaiseCanExecuteChanged method to check if the command can be executed
                OnCanExecuteChanged();
            }
        }

        // Method for checking if the command can be executed
        public override bool CanExecute(object? parameter)
            => !string.IsNullOrEmpty(_overviewViewModel.RoomNumberString)
            && !string.IsNullOrEmpty(_overviewViewModel.GuestName);


        // Async method for removing reservations from the hotel store and displaying a message box
        public override async Task ExecuteAsync(object? parameter)
        {
            bool wasRemoved = false;

            try
            {
                int roomNumber = 0;

                // Try to parse the room number from the input string
                if(int.TryParse(_overviewViewModel.RoomNumberString, out roomNumber))
                {
                    // Remove the reservation from the hotel store and get the result
                    wasRemoved = await _hotelStore.RemoveReservationByHotelStoreAsync(roomNumber, _overviewViewModel.GuestName);
                }
            }
            catch (Exception)
            {
                throw new Exception("Could not remove the reservation");
            }

            if(wasRemoved)
            {
                // Clear the input fields
                _overviewViewModel.RoomNumberString = string.Empty;
                _overviewViewModel.GuestName = string.Empty;

                // Update the current status of the rooms 
              
                // Display a message box with information about the removal of the reservation - successful
                MessageBox.Show("Reservation successfully removed", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Dispose the command after the reservation has been removed
                Dispose();

                _overviewViewModel.Dispose();
            }
            else
            {
                // Display a message box with information about the removal of the reservation - failed
                MessageBox.Show("Reservation could not be removed", "Error", 
                                       MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Unsubscribe from the PropertyChanged event
        public void Dispose()
        {
            _overviewViewModel.PropertyChanged -= OnViewModelChanged;
        }
    }
}
