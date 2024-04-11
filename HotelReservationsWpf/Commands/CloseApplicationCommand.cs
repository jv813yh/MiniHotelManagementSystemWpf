﻿using HotelReservationsWpf.Stores;

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

        // Save the current status of the rooms to .xml and close the application
        public override void Execute(object? parameter)
        {
            try
            {
                // Saving room statuses in .xml
                _hotelStore.SaveTheCurrentStatusOfTheRoomsToXmlHotelStore();

                // Close the application
                App.Current.Shutdown();

            }
            catch (Exception)
            {
                throw new Exception($"Could not save the current status of the rooms");
            }
        }
    }
}
