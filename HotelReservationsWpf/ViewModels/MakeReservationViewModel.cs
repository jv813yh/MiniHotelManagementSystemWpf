using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Stores;
using System.Windows;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
    {
        private readonly HotelStore _hotelStore;
        private RoomType? _roomType = null;

        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _emailAddress = string.Empty;
        private DateTime _checkInDate = DateTime.Now;
        private DateTime _checkOutDate = DateTime.Now;
        private string _expectedPriceString = string.Empty;

       
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public DateTime CheckInDate
        {
            get { return _checkInDate; }
            set
            {
                _checkInDate = value;
                OnPropertyChanged(nameof(CheckInDate));

                CalculateExpectedPrice(RoomTypeProperty);
            }
        }

        public DateTime CheckOutDate
        {
            get { return _checkOutDate; }
            set
            {
                _checkOutDate = value;

                OnPropertyChanged(nameof(CheckOutDate));

                CalculateExpectedPrice(RoomTypeProperty);
            }
        }

        public string PhomeNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhomeNumber));
            }
        }
        
        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;
                OnPropertyChanged(nameof(EmailAddress));
            }
        }

        public RoomType? RoomTypeProperty
        {
            get { return _roomType; }
            set
            {
                _roomType = value;
                OnPropertyChanged(nameof(RoomTypeProperty));

                CalculateExpectedPrice(RoomTypeProperty);
            }
        }

        public string ExpectedPriceString
        {
            get { return _expectedPriceString; }

            set
            {
                if(CheckOutDate.Day != DateTime.Now.Day && RoomTypeProperty != null
                    && !(CheckOutDate.DayOfYear < CheckInDate.DayOfYear) 
                        && !(CheckInDate.Day < DateTime.Now.Day))
                {
                    _expectedPriceString = value;
                    OnPropertyChanged(nameof(ExpectedPriceString));
                }
                else if(RoomTypeProperty != null)
                {
                    _expectedPriceString = "Unfeasible";
                    OnPropertyChanged(nameof(ExpectedPriceString));
                }
            }
        }


        public string GetStatusStandardRoomsProperty
        {
            get
            {
                (int available, int occupied) = _hotelStore.GetStatusStandardRoomsHotelStore();
                return $"Occupied: {occupied} Available: {available}";
            }
        }

        public string GetStatusDeluxeRoomsProperty
        {
            get
            {
                (int available, int occupied) = _hotelStore.GetStatusDeluxeRoomsHotelStore();
                return $"Occupied: {occupied} Available: {available}";
            }
        }

        public string GetStatusSuiteRoomsProperty
        {
            get
            {
                (int available, int occupied) = _hotelStore.GetStatusSuiteRoomsHotelStore();
                return $"Occupied: {occupied} Available: {available}";
            }
        }

        //
        public ICommand RoomPreferenceCommand { get; }

        //
        public ICommand SubmitCommand { get; }

        //
        public ICommand NavigateCommand { get; }

        public MakeReservationViewModel(HotelStore hotelStore, NavigationServiceWpf navigationServiceToReservationsListingViewModel)
        {
            _hotelStore = hotelStore;

            SubmitCommand = new MakeReservationCommand(hotelStore, this, 
                new NavigateCommand(navigationServiceToReservationsListingViewModel));

            RoomPreferenceCommand = new RelayCommand<string>(SetRoomType);
           // NavigateCommand = new NavigateCommand();
        }

        // Set the room type for the reservation 
        private void SetRoomType(string type)
        {
            switch (type)
            {
                case "Standard":
                    RoomTypeProperty = RoomType.Standard;
                    break;
                case "Deluxe":
                    RoomTypeProperty = RoomType.Deluxe;
                    break;
                case "Suite":
                    RoomTypeProperty = RoomType.Suite;
                    break;
                default:
                    MessageBox.Show("Invalid room type", "Wrong room type",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }

        // Calculate the expected price for the reservation
        private void CalculateExpectedPrice(RoomType? roomType)
        {
            if (roomType == null)
            {
                ExpectedPriceString = "0.00 €";

            }

            switch (RoomTypeProperty)
            {
                case RoomType.Standard:
                    ExpectedPriceString = (_hotelStore.GetPriceForStandardRoomHotelStore() * 
                                                (CheckOutDate.DayOfYear - CheckInDate.DayOfYear)).ToString("0.00") + " €";
                    break;
                case RoomType.Deluxe:
                    ExpectedPriceString = (_hotelStore.GetPriceForDeluxeRoomHotelStore() *
                                                (CheckOutDate.DayOfYear - CheckInDate.DayOfYear)).ToString("0.00") + " €";
                    break;
                case RoomType.Suite:
                    ExpectedPriceString = (_hotelStore.GetPriceForSuiteRoomHotelStore() *
                                                (CheckOutDate.DayOfYear - CheckInDate.DayOfYear)).ToString("0.00") + " €";
                    break;
                default:
                    ExpectedPriceString = "0.00 €";
                    break;
            }
        }
    }
}
