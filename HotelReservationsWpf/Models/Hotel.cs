using HotelReservationsWpf.Services.ReservationCreators;
using HotelReservationsWpf.Services.ReservationProviders;

namespace HotelReservationsWpf.Models
{
    public class Hotel
    {
        // Name of the hotel
        public string Name { get; }

        // Business logic of the hotel is handled by ManagementHotel
        private readonly ManagementHotel _managementHotel; 

        // Reservation creator and provider interfaces for creating and getting reservations 
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationProvider _reservationProvider;


        public Hotel(string name, int[] countOfRooms, decimal[] pricesPerNight,
                            IReservationCreator reservation, IReservationProvider reservationProvider) 
        {
            Name = name;

            _reservationCreator = reservation;
            _reservationProvider = reservationProvider;

            _managementHotel = new ManagementHotel(countOfRooms, pricesPerNight, 
                                                        _reservationCreator, _reservationProvider);
        }

        // Create a new reservation async and check for conflicts
        public async Task CreateReservationAsync(Reservation reservation)
        {
           await _managementHotel.CreateReservationAsync(reservation);
        }

        // Get all reservations async
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
         => await _managementHotel.GetAllReservationsAsync();

        // Get the statuses of rooms in the hotel
        public (int, int) GetStatusStandardRooms()
            => _managementHotel.GetStatusStandardRooms();

        public (int, int) GetStatusDeluxeRooms()
            => _managementHotel.GetStatusDeluxeRooms();
        public (int, int) GetStatusSuiteRooms()
            => _managementHotel.GetStatusSuiteRooms();

        // Check if there is a room available in the hotel for the selected room type
        public bool IsAvailablePreferenceRoom(RoomType? roomType)
            => _managementHotel.IsAvailablePreferenceRoom(roomType);

        // Get a room randomly from the hotel for the selected room type
        public Room? GetRoomRandomHotel(RoomType? roomType)
            => _managementHotel.GetRoomRandom(roomType);

        // Prices for individual rooms per night according to room type
        public decimal GetPriceForStandardRoom()
            => _managementHotel.PricePerNightStandardRoom;
        public decimal GetPriceForDeluxeRoom()
            => _managementHotel.PricePerNightDeluxeRoom;
        public decimal GetPriceForSuiteRoom()
            => _managementHotel.PricePerNightSuiteRoom;
    }
}
