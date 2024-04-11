using HotelReservationsWpf.Stores;
using HotelReservationsWpf.ViewModels;
using System.Windows;

namespace HotelReservationsWpf.Commands
{
    public class RemoveReservationCommand : AsyncCommandBase
    {
        private readonly HotelStore _hotelStore;
        private readonly OverviewViewModel _overviewViewModel;

        public RemoveReservationCommand(HotelStore hotelStore, OverviewViewModel overviewViewModel)
        {
            _hotelStore = hotelStore;
            _overviewViewModel = overviewViewModel;
        }

        // Async method for removing reservations from the hotel store and displaying a message box
        public override async Task ExecuteAsync(object? parameter)
        {
            bool wasRemoved = false;

            try
            {
                int roomNumber = 0;

                if(int.TryParse(_overviewViewModel.RoomNumberString, out roomNumber))
                {
                    wasRemoved = await _hotelStore.RemoveReservationHotelStoreAsync(roomNumber, _overviewViewModel.GuestName);
                }
            }
            catch (Exception)
            {
                throw new Exception("Could not remove the reservation");
            }

            if(wasRemoved)
            {

                _overviewViewModel.RoomNumberString = string.Empty;
                _overviewViewModel.GuestName = string.Empty;

                MessageBox.Show("Reservation successfully removed", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Reservation could not be removed", "Error", 
                                       MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
