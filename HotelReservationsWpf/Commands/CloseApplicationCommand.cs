using HotelReservationsWpf.Stores;
using HotelReservationsWpf.ViewModels;
using System.Windows;

namespace HotelReservationsWpf.Commands
{
    public class CloseApplicationCommand : CommandBase
    {
        // Fields
        private readonly HotelStore _hotelStore;
        private readonly OverviewViewModel _vieModel;

        public CloseApplicationCommand(HotelStore hotelStore, OverviewViewModel overviewViewModel)
        {
            _hotelStore = hotelStore;
            _vieModel = overviewViewModel;
        }

        // Save the current status of the rooms to .xml and close the application
        public override void Execute(object? parameter)
        {
            try
            {
                // Saving room statuses in .xml
                _hotelStore.SaveTheCurrentStatusOfTheRoomsToXmlByHotelStore();

                // Save the earnings to .xlsx
                _hotelStore.SaveTheMonthlyEarningsToExcelByHotelStore();

                // Thanks for using
               MessageBoxResult result = MessageBox.Show("Thank you for using the application!", "Hotel Reservations", 
                    MessageBoxButton.OK, MessageBoxImage.Information);

                _vieModel.Dispose();

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
