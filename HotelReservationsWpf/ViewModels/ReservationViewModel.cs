using HotelReservationsWpf.Models;

namespace HotelReservationsWpf.ViewModels
{
    // I am using an ObservableCollection to display the reservations in the ReservationsView,
    // they need to implement INotifyPropertyChanged to ensure that the view updates work correctly,
    // so this class is used for this purpose
    public class ReservationViewModel : ViewModelBase
    {
        private Reservation _reservation;
        public int RoomNumber => _reservation.CurrentRoom.RoomNumber;
        public RoomType RoomType => _reservation.CurrentRoom.RoomType;
        public string GuestName => _reservation.GuestName.FirstName + " " + _reservation.GuestName.LastName;
        public DateOnly CheckInDate => _reservation.CheckInDate;
        public DateOnly CheckOutDate => _reservation.CheckOutDate;
        public decimal TotalCost => _reservation.TotalCost;

        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;
        }
    }
}
