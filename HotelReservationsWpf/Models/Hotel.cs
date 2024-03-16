using HotelReservationsWpf.Services.InitializationRoomsProvider;

namespace HotelReservationsWpf.Models
{
    public class Hotel
    {
        public string Name { get; }

        private readonly ReservationsBook _reservationBooks;

        private readonly List<Room> _rooms;


        public Hotel(string name) 
        {
            Name = name;

            IInitializationRooms initializationRooms = new InitializationRooms(40,65,90);
            _rooms = initializationRooms.GetRooms();

            _reservationBooks = new ReservationsBook();
        }

        public void AddReservation(Reservation reservation)
        {
            _reservationBooks.MakeReservation(reservation);
        }

        public IEnumerable<Reservation> GetAllReservations()
         => _reservationBooks.GetAllReservations();

        public (int, int) GetStatusStandardRooms()
        {
            int available = _rooms.Count(r => r.RoomType == RoomType.Standard && r.RoomStatus == RoomStatus.Available);
            int occupied = _rooms.Count(r => r.RoomType == RoomType.Standard && r.RoomStatus == RoomStatus.Occupied);


            return (available,occupied);
        }

        public (int, int) GetStatusDeluxeRooms()
        {
            int available = _rooms.Count(r => r.RoomType == RoomType.Deluxe && r.RoomStatus == RoomStatus.Available);
            int occupied = _rooms.Count(r => r.RoomType == RoomType.Deluxe && r.RoomStatus == RoomStatus.Occupied);

            return (available, occupied);
        }

        public (int, int) GetStatusSuiteRooms()
        {
            int available = _rooms.Count(r => r.RoomType == RoomType.Suite && r.RoomStatus == RoomStatus.Available);
            int occupied = _rooms.Count(r => r.RoomType == RoomType.Suite && r.RoomStatus == RoomStatus.Occupied);

            return (available, occupied);
        }

        public bool IsAvailablePreferenceRoom(RoomType? roomType)
        {
            if(roomType == null)
            {
                return false;
            }

            return _rooms.Any(r => r.RoomType == roomType && r.RoomStatus == RoomStatus.Available);
        }

    }
}
