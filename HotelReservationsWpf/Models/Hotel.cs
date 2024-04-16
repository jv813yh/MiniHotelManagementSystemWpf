using HotelReservationsWpf.Services.InitializationRoomsProviders;
using HotelReservationsWpf.Services.ReservationCreators;
using HotelReservationsWpf.Services.ReservationProviders;
using HotelReservationsWpf.Services.ReservationRemovers;
using HotelReservationsWpf.Services.SaveRoomsProviders;

namespace HotelReservationsWpf.Models
{
    public class Hotel
    {
        // Name of the hotel
        public string Name { get; }

        // Business logic of the hotel is handled by ManagementHotel
        private readonly ManagementHotel _managementHotel; 

        public Hotel(string name, int[] countOfRooms, decimal[] pricesPerNight,
                            IReservationCreator reservationCreator, IReservationProvider reservationProvider,
                            IReservationRemover reservationRemover,
                            IInitializationRooms initializationRooms, ISaveRoomsProvider saveRooms) 
        {
            Name = name;

            _managementHotel = new ManagementHotel(countOfRooms, pricesPerNight,
                                                  reservationCreator, reservationProvider,
                                                  reservationRemover,
                                                  initializationRooms, saveRooms);
        }

        public decimal TotalIncome
            => _managementHotel.TotalIncome;

        // Create a new reservation async and check for conflicts
        public async Task CreateReservationAsync(Reservation reservation)
            => await _managementHotel.CreateReservationInReservationBookAsync(reservation);
        

        // Get all reservations async
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
         => await _managementHotel.GetAllReservationsFromReservationBookAsync();

        // Remove a reservation async
        public async Task<(bool, RoomType)> RemoveReservationAsync(int roomNumber, string guestName)
            => await _managementHotel.RemoveReservationFromReservationBookAsync(roomNumber, guestName);

        // 
        public void SaveTheCurrentStatusOfTheRoomsToXml()
            => _managementHotel.SaveRoomsWithPricesToXml();

        public void UpdateRoomStatus(int roomNumber, RoomType roomType)
            => _managementHotel.UpdateRoomStatus(roomNumber, roomType);


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
