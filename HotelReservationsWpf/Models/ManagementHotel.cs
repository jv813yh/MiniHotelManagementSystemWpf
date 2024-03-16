using HotelReservationsWpf.Services.InitializationRoomsProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationsWpf.Models
{
    public class ManagementHotel
    {
        //
        private readonly ReservationsBook _reservationBook;

        //
        private readonly List<Room> _rooms;

        // Prices for individual rooms per night according to room type
        public decimal PricePerNightStandardRoom { get; }
        public decimal PricePerNightDeluxeRoom { get; }
        public decimal PricePerNightSuiteRoom { get; }

        public ManagementHotel(decimal pricePerNightStandardRoom,
                 decimal pricePerNightDexlureRoom, decimal pricePerNightSuiteRoom)
        {
            PricePerNightStandardRoom = pricePerNightStandardRoom;
            PricePerNightDeluxeRoom = pricePerNightDexlureRoom;
            PricePerNightSuiteRoom = pricePerNightSuiteRoom;

            IInitializationRooms initializationRooms = new InitializationRooms(PricePerNightStandardRoom,
                        PricePerNightDeluxeRoom, PricePerNightSuiteRoom);

            _rooms = initializationRooms.GetRooms();

            _reservationBook = new ReservationsBook();
        }

        public void AddReservation(Reservation reservation)
        {
            _reservationBook.MakeReservation(reservation);
        }

        public IEnumerable<Reservation> GetAllReservations()
         => _reservationBook.GetAllReservations();

        public (int, int) GetStatusStandardRooms()
        {
            int available = _rooms.Count(r => r.RoomType == RoomType.Standard && r.RoomStatus == RoomStatus.Available);
            int occupied = _rooms.Count(r => r.RoomType == RoomType.Standard && r.RoomStatus == RoomStatus.Occupied);


            return (available, occupied);
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
            if (roomType == null)
            {
                return false;
            }

            return _rooms.Any(r => r.RoomType == roomType && r.RoomStatus == RoomStatus.Available);
        }
    }
}
