using HotelReservationsWpf.Stores;
using HotelReservationsWpf.ViewModels;
using System.Windows;

namespace HotelReservationsWpf.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly HotelStore _hotelStore;
        private readonly ReservationsListingViewModel _reservationsListingViewModel;

        public LoadReservationsCommand(HotelStore hotelStore, ReservationsListingViewModel reservationsListingViewModel)
        {
            _hotelStore = hotelStore;
            _reservationsListingViewModel = reservationsListingViewModel;
        }

        // Load all reservations from the database async and load them to the view model
        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {

                // Get all reservations from the database async
                await _hotelStore.LoadReservationsByHotelStoreAsync();

                // Load reservations to the view model
                _reservationsListingViewModel.LoadReservationsFromDb(_hotelStore.Reservations);

            }
            catch (Exception)
            {

                MessageBox.Show("Error loading reservations from the database", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            await Task.Delay(2000);

            // Check if the reservations collection is empty
            _reservationsListingViewModel.IsReservationsEmpty = !_hotelStore.Reservations.Any();

            _reservationsListingViewModel.IsNeededListView = !_reservationsListingViewModel.IsReservationsEmpty;

            // Set the loading spinner to false
            _reservationsListingViewModel.IsLoadingSpinner = false;

        }
    }
}
