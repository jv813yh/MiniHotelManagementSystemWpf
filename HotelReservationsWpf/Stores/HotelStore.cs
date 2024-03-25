using HotelReservationsWpf.Models;

namespace HotelReservationsWpf.Stores
{
    /// <summary>
    /// Store is important for managing application state + lazy loading data
    /// </summary>
    public class HotelStore
    {
        // Hotel instance
        private readonly Hotel _hotel; 
        private readonly List<Reservation> _reservations;

        // Lazy initialization of reservations
        private readonly Lazy<Task> _initializeLazy;

        //
        public IEnumerable<Reservation> Reservations => _reservations;

        public HotelStore(Hotel hotel)
        {
            _hotel = hotel;

            _reservations = new List<Reservation>();

            _initializeLazy = new Lazy<Task>(InitializeLazyLoadReservationsHotelStoreAsync);
        }

        /// <summary>
        /// Creates reservation using the hotel instance and adds it to the reservation list, 
        /// which is displayed in the ReservationsView
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        public async Task CreateReservationHotelStoreAsync(Reservation reservation)
        {
            await _hotel.CreateReservationAsync(reservation);    

            _reservations.Add(reservation);
        }

        /// <summary>
        /// Initialize lazy load reservations
        /// Initializes only once, when its value is first requested
        /// </summary>
        /// <returns></returns>
        public async Task LoadReservationsHotelStoreAsync()
        {
            // Initializes only once, when its value is first requested
            await _initializeLazy.Value;
        }

        /// <summary>
        /// Impelementation of lazy load reservations
        /// </summary>
        /// <returns></returns>
        private async Task InitializeLazyLoadReservationsHotelStoreAsync()
        {
            IEnumerable<Reservation> reservations = await _hotel.GetAllReservationsAsync();

            _reservations.Clear();
            _reservations.AddRange(reservations);
        }

        public string HotelName 
        { 
            get => _hotel.Name;
        }

        // Get the statuses of rooms in the hotel
        public (int, int) GetStatusStandardRoomsHotelStore()
            => _hotel.GetStatusStandardRooms();

        public (int, int) GetStatusDeluxeRoomsHotelStore()
            => _hotel.GetStatusDeluxeRooms();
        public (int, int) GetStatusSuiteRoomsHotelStore()
            => _hotel.GetStatusSuiteRooms();

        // Check if there is a room available in the hotel for the selected room type
        public bool IsAvailablePreferenceRoomHotelStore(RoomType? roomType)
            => _hotel.IsAvailablePreferenceRoom(roomType);

        // Get a room randomly from the hotel for the selected room type
        public Room? GetRoomRandomHotelStore(RoomType? roomType)
            => _hotel.GetRoomRandomHotel(roomType);

        // Prices for individual rooms per night according to room type
        public decimal GetPriceForStandardRoomHotelStore()
            => _hotel.GetPriceForStandardRoom(); 
        public decimal GetPriceForDeluxeRoomHotelStore()
            => _hotel.GetPriceForDeluxeRoom();
        public decimal GetPriceForSuiteRoomHotelStore()
            => _hotel.GetPriceForSuiteRoom();
    }
}
