using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Stores;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class OverviewViewModel : ViewModelBase
    {
        // Fields
        private readonly HotelStore _hotelStore;

        // Room number and guest name properties for removing reservations
        private string _roomNumberString = string.Empty;
        public string RoomNumberString
        {
            get => _roomNumberString;
            set
            {
                _roomNumberString = value;
                OnPropertyChanged(nameof(RoomNumberString));
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

        public SeriesCollection RoomSeries { get; set; }

        public string[] RoomTypeString  { get; set; }
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

            UpdateRoomStatus();

        }

        private void UpdateRoomStatus()
        {
            RoomSeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Free Standard Rooms",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_hotelStore.GetStatusStandardRoomsHotelStore().Item1) } 
                },
                new ColumnSeries
                {
                    Title = "Occupied Standard Rooms",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_hotelStore.GetStatusStandardRoomsHotelStore().Item2) } 
                },

                new ColumnSeries
                {
                    Title = "Free Deluxe Rooms",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_hotelStore.GetStatusDeluxeRoomsHotelStore().Item1) }
                },
                new ColumnSeries
                {
                    Title = "Occupied Deluxe Rooms",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_hotelStore.GetStatusDeluxeRoomsHotelStore().Item2) }
                },

                new ColumnSeries
                {
                    Title = "Free Suite Rooms",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_hotelStore.GetStatusSuiteRoomsHotelStore().Item1) }
                },
                new ColumnSeries
                {
                    Title = "Occupied Suite Rooms",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_hotelStore.GetStatusSuiteRoomsHotelStore().Item2) }
                },
            };

            RoomTypeString = new string[] { "Standard", "Deluxe", "Suite" };
        }
    }
}
