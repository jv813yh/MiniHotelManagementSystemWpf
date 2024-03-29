using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Stores;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class OverviewViewModel : ViewModelBase
    {
        // Fields
        private readonly HotelStore _hotelStore;

        // Commands for navigating to the reservation creation view and the reservation listing view
        public ICommand NavigateToMakeReservationCommand { get; }
        public ICommand NavigateToReservationsListingCommand { get; }
        public ICommand CloseApplicationCommand { get; }


        // Constructor
        public OverviewViewModel(HotelStore hotelStore, NavigationServiceWpf navigateServiceMakeResv,
                    NavigationServiceWpf navigateServiceToResvListing)
        {
            _hotelStore = hotelStore;
            NavigateToMakeReservationCommand = new NavigateCommand(navigateServiceMakeResv);
            NavigateToReservationsListingCommand = new NavigateCommand(navigateServiceToResvListing);
            CloseApplicationCommand = new CloseApplicationCommand(_hotelStore);
        }
    }
}
