using HotelReservationsWpf.Stores;

namespace HotelReservationsWpf.Commands
{
    public class CloseApplicationCommand : CommandBase
    {
        // Fields
        private readonly HotelStore _hotelStore;

        public CloseApplicationCommand(HotelStore hotelStore)
        {
            _hotelStore = hotelStore;
        }
        public override void Execute(object? parameter)
        {
            if(_hotelStore.Reservations.Any())
            {
                // Saving room statuses in .xml

            }

            // Close the application
            App.Current.Shutdown();

        }
    }
}
