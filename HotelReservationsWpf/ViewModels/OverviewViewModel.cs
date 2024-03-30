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

        // Room number and guest name properties for removing reservations
        private int _roomNumber;
        public int RoomNumber
        {
            get => _roomNumber;
            set
            {
                _roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }

        private string _guestName = string.Empty;
        public string GuestName
        {
            get => _guestName;
            set
            {
                _guestName = value;
                OnPropertyChanged(nameof(GuestName));
            }
        }

        public string HotelName
            => _hotelStore.HotelName;

        // Commands for navigating to the reservation creation view and the reservation listing view
        public ICommand NavigateToMakeReservationCommand { get; }
        public ICommand NavigateToReservationsListingCommand { get; }

        // Command for removing reservations
        public ICommand RemoveReservationCommand { get; }

        // Command for closing the application
        public ICommand CloseApplicationCommand { get; }


        // Constructor
        public OverviewViewModel(HotelStore hotelStore, NavigationServiceWpf navigateServiceMakeResv,
                    NavigationServiceWpf navigateServiceToResvListing)
        {
            _hotelStore = hotelStore;

            NavigateToMakeReservationCommand = new NavigateCommand(navigateServiceMakeResv);
            NavigateToReservationsListingCommand = new NavigateCommand(navigateServiceToResvListing);

            RemoveReservationCommand = new RemoveReservationCommand(_hotelStore, this);

            CloseApplicationCommand = new CloseApplicationCommand(_hotelStore);
        }
    }
}
