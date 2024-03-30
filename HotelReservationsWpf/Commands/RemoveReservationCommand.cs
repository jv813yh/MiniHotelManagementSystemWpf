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

        public override async Task ExecuteAsync(object? parameter)
        {
            bool wasRemoved = true;
            try
            {
                wasRemoved = await _hotelStore.RemoveReservationHotelStoreAsync(_overviewViewModel.RoomNumber, _overviewViewModel.GuestName);
            }
            catch (Exception)
            {
                throw new Exception("Could not remove the reservation");
            }

            if(wasRemoved)
            {
                MessageBox.Show("Reservation removed", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
