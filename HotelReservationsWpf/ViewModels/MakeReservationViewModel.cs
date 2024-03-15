using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Models;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
    {
        private readonly Hotel _hotel;
        private RoomType _roomType;

        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _emailAddress = string.Empty;
        private DateTime _checkInDate = DateTime.Now;
        private DateTime _checkOutDate = DateTime.Now;

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
                OnPropertyChanged(nameof(CheckOutDate));
            }
        }

        public RoomType RoomType
        {
            get { return _roomType; }
            set
            {
                _roomType = value;
                OnPropertyChanged(nameof(RoomType));
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

        public string GetStatusStandardRoomsProperty
        {
            get
            {
                var (available, occupied) = _hotel.GetStatusStandardRooms();
                return $"Occupied: {occupied} Available: {available}";
            }
        }

        public string GetStatusDeluxeRoomsProperty
        {
            get
            {
                var (available, occupied) = _hotel.GetStatusDeluxeRooms();
                return $"Occupied: {occupied} Available: {available}";
            }
        }

        public string GetStatusSuiteRoomsProperty
        {
            get
            {
                var (available, occupied) = _hotel.GetStatusSuiteRooms();
                return $"Occupied: {occupied} Available: {available}";
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand NavigateCommand { get; }

        public MakeReservationViewModel(Hotel hotel)
        {
            _hotel = hotel;

            SubmitCommand = new MakeReservationCommand(hotel, this, new NavigateCommand());
            NavigateCommand = new NavigateCommand();
        }
    }
}
