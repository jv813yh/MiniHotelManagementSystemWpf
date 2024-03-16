using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Models;
using System.Windows;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
    {
        private readonly Hotel _hotel;
        private RoomType? _roomType = null;

        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _emailAddress = string.Empty;
        private DateTime _checkInDate = DateTime.Now;
        private DateTime _checkOutDate = DateTime.Now;
        private string _expectedPriceString = string.Empty;

        public Hotel Hotel
        {
            get => _hotel;
        }
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
            }
        }

        public DateTime CheckOutDate
        {
            get { return _checkOutDate; }
            set
            {
                _checkOutDate = value;

                if(RoomTypeProperty != null && CheckOutDate.DayOfYear > DateTime.Now.DayOfYear)
                {
                    CalculateExpectedPrice(RoomTypeProperty);
                }

                OnPropertyChanged(nameof(CheckOutDate));
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

                if (CheckOutDate.Day != DateTime.Now.Day)
                {
                    CalculateExpectedPrice(RoomTypeProperty);
                }

                OnPropertyChanged(nameof(RoomTypeProperty));
            }
        }

        public string ExpectedPriceString
        {
            get { return _expectedPriceString; }

            set
            {
                _expectedPriceString = value;
                OnPropertyChanged(nameof(ExpectedPriceString));
            }
        }


        public string GetStatusStandardRoomsProperty
        {
            get
            {
                (int available, int occupied) = _hotel.GetStatusStandardRooms();
                return $"Occupied: {occupied} Available: {available}";
            }
        }

        public string GetStatusDeluxeRoomsProperty
        {
            get
            {
                (int available, int occupied) = _hotel.GetStatusDeluxeRooms();
                return $"Occupied: {occupied} Available: {available}";
            }
        }

        public string GetStatusSuiteRoomsProperty
        {
            get
            {
                (int available, int occupied) = _hotel.GetStatusSuiteRooms();
                return $"Occupied: {occupied} Available: {available}";
            }
        }

        //
        public ICommand RoomPreferenceCommand { get; }

        //
        public ICommand SubmitCommand { get; }

        //
        public ICommand NavigateCommand { get; }

        public MakeReservationViewModel(Hotel hotel)
        {
            _hotel = hotel;

            SubmitCommand = new MakeReservationCommand(hotel, this, new NavigateCommand());
            RoomPreferenceCommand = new RelayCommand<string>(ExecuteRoomType);
            NavigateCommand = new NavigateCommand();
        }

        // Set the room type for the reservation 
        private void ExecuteRoomType(string type)
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
                    ExpectedPriceString = (_hotel.GetPriceForStandardRoom() * 
                                                (CheckOutDate.DayOfYear - CheckInDate.DayOfYear)).ToString("0.00") + " €";
                    break;
                case RoomType.Deluxe:
                    ExpectedPriceString = (_hotel.GetPriceForDeluxeRoom() *
                                                (CheckOutDate.DayOfYear - CheckInDate.DayOfYear)).ToString("0.00") + " €";
                    break;
                case RoomType.Suite:
                    ExpectedPriceString = (_hotel.GetPriceForSuiteRoom() *
                                                (CheckOutDate.DayOfYear - CheckInDate.DayOfYear)).ToString("0.00") + " €";
                    break;
                default:
                    ExpectedPriceString = "0.00 €";
                    break;
            }
        }
    }
}
