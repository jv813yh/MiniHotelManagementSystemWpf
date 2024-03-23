namespace HotelReservationsWpf.Models
{
    public class Hotel
    {
        // Name of the hotel
        public string Name { get; }

        // Business logic of the hotel is handled by ManagementHotel
        private readonly ManagementHotel _managementHotel; 


        public Hotel(string name, int[] countOfRooms, decimal[] pricesPerNight) 
        {
            Name = name;
            _managementHotel = new ManagementHotel(countOfRooms, pricesPerNight);
        }

        public void CreateReservation(Reservation reservation)
        {
            _managementHotel.AddReservation(reservation);
        }

        public IEnumerable<Reservation> GetAllReservations()
         => _managementHotel.GetAllReservations();

        public (int, int) GetStatusStandardRooms()
            => _managementHotel.GetStatusStandardRooms();

        public (int, int) GetStatusDeluxeRooms()
            => _managementHotel.GetStatusDeluxeRooms();

        public (int, int) GetStatusSuiteRooms()
            => _managementHotel.GetStatusSuiteRooms();

        public bool IsAvailablePreferenceRoom(RoomType? roomType)
            => _managementHotel.IsAvailablePreferenceRoom(roomType);

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
