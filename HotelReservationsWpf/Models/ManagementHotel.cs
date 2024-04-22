using HotelReservationsWpf.DTOs;
using HotelReservationsWpf.Services.EarningsReadingProvider;
using HotelReservationsWpf.Services.EarningsWritingProvider;
using HotelReservationsWpf.Services.InitializationRoomsProviders;
using HotelReservationsWpf.Services.ReservationCreators;
using HotelReservationsWpf.Services.ReservationProviders;
using HotelReservationsWpf.Services.ReservationRemovers;
using HotelReservationsWpf.Services.SaveRoomsProviders;
using System.IO;
using System.Windows;

namespace HotelReservationsWpf.Models
{
    // Class for managing the hotel with the ability to create reservations, get all reservations,
    // delete reservations, get the status of rooms in the hotel, check if there is a room available for reservation ...
    public class ManagementHotel
    {
        // File names for the overview of rooms for each type of room in the hotel
        private readonly string _overviewStandardRoomsString = 
                       Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "overviewOfStandardRoomsFile.xml"),
                       _overviewDeluxeRoomsString = 
                       Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "overviewOfDeluxeRoomsFile.xml"),
                       _overviewSuiteRoomsString = 
                       Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "overviewOfSuiteRoomsFile.xml");

        private readonly string _monthlyEarningsString = 
                       Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "monthlyEarnings.xlsx");

        // Reservation book for managing reservations in the hotel
        private readonly ReservationsBook _reservationBook;

        // Services for saving rooms
        private readonly ISaveRoomsProvider _saveRoomsProvider;

        // Lists of rooms for each type of room in the hotel (standard, deluxe, suite) 
        // with information about rooms in the hotel
        private  List<Room> _standardRooms;
        private  List<Room> _deluxeRooms;
        private  List<Room> _suiteRooms;

        // Prices for individual rooms per night according to room type
        public decimal PricePerNightStandardRoom { get; private set; }
        public decimal PricePerNightDeluxeRoom { get; private set; }
        public decimal PricePerNightSuiteRoom { get; private set; }

        // Total income from reservations in the hotel, after removing a reservation
        public decimal TotalIncome { get; private set;}

        public IEarningsWritting EarningsWritting { get; private set; }
        public IEarningReading EarningReading { get; private set; }

        public List<MontlyEarningsDTO> MontlyEarnings { get; private set; }

        public ManagementHotel(int[] countOfRooms, decimal[] pricesPerNightRooms, 
                                    IReservationCreator reservationCreator, IReservationProvider reservationProvider,
                                    IReservationRemover reservationRemover,
                                    IInitializationRooms initializationRooms, ISaveRoomsProvider saveRooms,
                                    IEarningsWritting earningsWritting, IEarningReading earningReading)
        {
            _reservationBook = new ReservationsBook(reservationCreator, reservationProvider, reservationRemover);

            _saveRoomsProvider = saveRooms;

            CreateRoomsWithPrices(initializationRooms, countOfRooms, pricesPerNightRooms);

            MontlyEarnings = new List<MontlyEarningsDTO>();

            EarningsWritting = earningsWritting;

            EarningReading = earningReading;

        }

        // Create rooms with prices per night for each type of room in the hotel
        // and initialize the list of rooms for each type of room 
        // with the number of rooms and the price per night ...
        public void CreateRoomsWithPrices(IInitializationRooms initializationRooms, int[] countOfRooms, 
                            decimal[] pricesPerNightRoom)
        {
            if(countOfRooms.Length > 3 || pricesPerNightRoom.Length > 3)
            {
                throw new ArgumentException("The hotel currently offers a maximum of 3 types of rooms");
            }

            if(countOfRooms.Length != pricesPerNightRoom.Length)
            {
                throw new ArgumentException("The number type of rooms and prices per night must be the same");
            }
            else
            {
                for (int i = 0; i < countOfRooms.Length; i++)
                {
                    if (countOfRooms[i] <= 0)
                    {
                        throw new ArgumentException("The number of rooms must be greater than 0");
                    } 
                    else if(countOfRooms[i] > 0)
                    {

                        if(i == 0)
                        {
                            PricePerNightStandardRoom = pricesPerNightRoom[i];
                            // Initialize the list of standard rooms with the number of rooms and the price per night
                            _standardRooms = initializationRooms.ExecuteInitializeRoomFromXml(_overviewStandardRoomsString, 
                                    RoomType.Standard, countOfRooms[i], PricePerNightStandardRoom);
                        } 
                        else if(i == 1)
                        {
                            PricePerNightDeluxeRoom = pricesPerNightRoom[i];
                            // Initialize the list of standard rooms with the number of rooms and the price per night
                            _deluxeRooms = initializationRooms.ExecuteInitializeRoomFromXml(_overviewDeluxeRoomsString,
                                    RoomType.Deluxe, countOfRooms[i], PricePerNightDeluxeRoom);
                        }
                        else if(i == 2)
                        {
                            PricePerNightSuiteRoom = pricesPerNightRoom[i];
                            // Initialize the list of standard rooms with the number of rooms and the price per night
                            _suiteRooms = initializationRooms.ExecuteInitializeRoomFromXml(_overviewSuiteRoomsString,
                                    RoomType.Suite, countOfRooms[i], PricePerNightSuiteRoom);
                        }
                    }
                }
            }
        }

        // Save the current status of the rooms in the hotel to the XML file
        public void SaveRoomsWithPricesToXml()
        {
            if (_standardRooms != null) 
            { 
                _saveRoomsProvider.ExecuteSaveRoomToXml(_overviewStandardRoomsString, _standardRooms); 
            }
            if (_deluxeRooms != null)
            {
                _saveRoomsProvider.ExecuteSaveRoomToXml(_overviewDeluxeRoomsString, _deluxeRooms); 
            }
            if (_suiteRooms != null)
            {
                _saveRoomsProvider.ExecuteSaveRoomToXml(_overviewSuiteRoomsString, _suiteRooms); 
            }

            //if (_standardRooms.Any(r => r.RoomStatus == RoomStatus.Occupied))
        }

        // Create a new reservation asynchronously and add it to the reservation book
        public async Task CreateReservationInReservationBookAsync(Reservation reservation)
        {
            await _reservationBook.MakeReservationAsync(reservation);
        }

        // Get all reservations asynchronously from the reservation book
        public async Task<IEnumerable<Reservation>> GetAllReservationsFromReservationBookAsync()
         => await _reservationBook.GetAllReservationsAsync();

        // Remove a reservation asynchronously from the reservation book
        public async Task<(bool, RoomType)> RemoveReservationFromReservationBookAsync(int roomNumber, string guestName)
        {
            var (wasRemoved, roomType, totalIncome) = await _reservationBook.RemoveReservationAsync(roomNumber, guestName);

            // Update the total income from reservations in the hotel after removing a reservation
            TotalIncome += totalIncome;

            return (wasRemoved, roomType);
        }

        /// <summary>
        /// Write the earnings to the Excel file
        /// </summary>
        public void ExcelWriteEarnings()
        {
            try
            {
                // If the earnings for the current month are already in the list, update the earnings
                if (MontlyEarnings[MontlyEarnings.Count - 1].Month == DateTime.Now.ToString("MMMM"))
                {
                    MontlyEarnings[MontlyEarnings.Count - 1].Earnings = TotalIncome;
                }
                else
                {
                    // If the earnings for the current month are not in the list, add the earnings
                    MontlyEarnings.Add(new MontlyEarningsDTO { Month = DateTime.Now.ToString("MMMM"), Earnings = TotalIncome });
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error writting data to Excel", "Error",
                                      MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Write the earnings to the Excel file
            EarningsWritting.WriteEarnings(MontlyEarnings, _monthlyEarningsString);
        }

        // Get the monthly earnings from the Excel file and update the total income for the current month
        public void GetMontlyEarnings()
        {
            // Read the earnings from the Excel file
            MontlyEarnings = EarningReading.ReadEarnings(_monthlyEarningsString);

            try
            {
                if (MontlyEarnings.Count > 0)
                {
                    if (MontlyEarnings[MontlyEarnings.Count - 1].Month == DateTime.Now.ToString("MMMM"))
                    {
                        TotalIncome = MontlyEarnings[MontlyEarnings.Count - 1].Earnings;
                    }
                    else
                    {
                        TotalIncome = 0;
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error reading data from Excel", "Error",
                                       MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // After removing a reservation, update the status of the room to available
        public void UpdateRoomStatus(int roomNumber, RoomType roomType)
        {
            Room? room = null;

            switch (roomType)
            {
                case RoomType.Standard:

                    room = _standardRooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

                    if (room != null)
                    {
                        room.RoomStatus = RoomStatus.Available;
                    }

                    break;

                case RoomType.Deluxe:

                    room = _deluxeRooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

                    if (room != null)
                    {
                        room.RoomStatus = RoomStatus.Available;
                    }

                    break;

                case RoomType.Suite:

                    room = _suiteRooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

                    if (room != null)
                    {
                        room.RoomStatus = RoomStatus.Available;
                    }

                    break;

                default:
                    break;
            }
        }


        // Get the status available and occupied for a standard rooms
        public (int, int) GetStatusStandardRooms()
        {
            int available = _standardRooms.Count(r=> r.RoomStatus == RoomStatus.Available);
            int occupied = _standardRooms.Count(r=> r.RoomStatus == RoomStatus.Occupied);


            return (available, occupied);
        }

        // Get the status available and occupied for a deluxe rooms
        public (int, int) GetStatusDeluxeRooms()
        {
            int available = _deluxeRooms.Count(r => r.RoomStatus == RoomStatus.Available);
            int occupied = _deluxeRooms.Count(r => r.RoomStatus == RoomStatus.Occupied);

            return (available, occupied);
        }

        // Get the status available and occupied for a suite rooms
        public (int, int) GetStatusSuiteRooms()
        {
            int available = _suiteRooms.Count(r => r.RoomStatus == RoomStatus.Available);
            int occupied = _suiteRooms.Count(r => r.RoomStatus == RoomStatus.Occupied);

            return (available, occupied);
        }

        // Check if there is a room available for the selected room type
        public bool IsAvailablePreferenceRoom(RoomType? roomType)
        {
            if (roomType == null)
            {
                return false;
            }

            switch(roomType)
            {
                case RoomType.Standard:
                    return _standardRooms.Any(r => r.RoomStatus == RoomStatus.Available);
                case RoomType.Deluxe:
                    return _deluxeRooms.Any(r => r.RoomStatus == RoomStatus.Available);
                case RoomType.Suite:
                    return _suiteRooms.Any(r => r.RoomStatus == RoomStatus.Available);
                default:
                    return false;
            }
        }

        // Get a random room of the selected type that is available for reservation 
        public Room? GetRoomRandom(RoomType? roomType)
        {
            if (roomType == null)
            {
                return null;
            }

            switch (roomType)
            {
                case RoomType.Standard:
                    return _standardRooms.FirstOrDefault(r => r.RoomStatus == RoomStatus.Available);
                case RoomType.Deluxe:
                    return _deluxeRooms.FirstOrDefault(r => r.RoomStatus == RoomStatus.Available);
                case RoomType.Suite:
                    return _suiteRooms.FirstOrDefault(r => r.RoomStatus == RoomStatus.Available);
                default:
                    return null;
            }
        }
    }
}
