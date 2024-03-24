using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services.ReservationProviders;
using HotelReservationsWpf.ViewModels;
using System.Windows;

namespace HotelReservationsWpf.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly ReservationsListingViewModel _reservationsListingViewModel;

        public LoadReservationsCommand(IReservationProvider reservationProvider, ReservationsListingViewModel reservationsListingViewModel)
        {
            _reservationProvider = reservationProvider;
            _reservationsListingViewModel = reservationsListingViewModel;
        }

        // Load all reservations from the database async and load them to the view model
        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                // Get all reservations from the database async
                IEnumerable<Reservation> reservationsFromDb = await _reservationProvider.GetAllReservationsAsync();

                // Load reservations to the view model
                _reservationsListingViewModel.LoadReservationsFromDb(reservationsFromDb);

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
