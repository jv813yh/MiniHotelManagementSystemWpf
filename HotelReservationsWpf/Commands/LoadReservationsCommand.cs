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
                await _hotelStore.LoadReservationsHotelStoreAsync();

                // Load reservations to the view model
                _reservationsListingViewModel.LoadReservationsFromDb(_hotelStore.Reservations);

                //_reservationsListingViewModel.LoadReservationsFromDb(await _reservationProvider.GetAllReservationsAsync());
            }
            catch (Exception)
            {

                MessageBox.Show("Error loading reservations from the database", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
