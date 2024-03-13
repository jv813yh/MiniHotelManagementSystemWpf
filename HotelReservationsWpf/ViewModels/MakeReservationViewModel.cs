using HotelReservationsWpf.Models;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
    {
        private readonly Hotel _hotel;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _address = string.Empty;
        private string _city = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _emailAddress = string.Empty;
        private DateTime _checkInDate = DateTime.MinValue;
        private DateTime _checkOutDate = DateTime.MinValue;
        private RoomType _roomType;

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

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
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

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public ICommand SubmitCommand { get; }

        public MakeReservationViewModel(Hotel hotel)
        {
            _hotel = hotel;
        }
    }
}
