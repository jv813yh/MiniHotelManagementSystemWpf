﻿using HotelReservationsWpf.Services.InitializationRoomsProvider;
using HotelReservationsWpf.Services.ReservationCreators;
using HotelReservationsWpf.Services.ReservationProviders;

namespace HotelReservationsWpf.Models
{


    public class ManagementHotel
    {
        // File names for the overview of rooms for each type of room in the hotel
        private string _overviewStandardRoomsString = "overviewOfStandardRoomsFile.xml",
                      _overviewDeluxeRoomsString = "overviewOfDeluxeRoomsFile.xml",
                      _overviewSuiteRoomsString = "overviewOfSuiteRoomsFile.xml";

        // Reservation book for managing reservations in the hotel
        private readonly ReservationsBook _reservationBook;

        // Lists of rooms for each type of room in the hotel (standard, deluxe, suite) 
        // with information about rooms in the hotel
        private  List<Room> _standardRooms;
        private  List<Room> _deluxeRooms;
        private  List<Room> _suiteRooms;

        // Prices for individual rooms per night according to room type
        public decimal PricePerNightStandardRoom { get; private set; }
        public decimal PricePerNightDeluxeRoom { get; private set; }
        public decimal PricePerNightSuiteRoom { get; private set; }

        public ManagementHotel(int[] countOfRooms, decimal[] pricesPerNightRooms, 
                                    IReservationCreator reservationCreator, IReservationProvider reservationProvider)
        {
            _reservationBook = new ReservationsBook(reservationCreator, reservationProvider);
            IInitializationRooms initializationRooms = new InitializationRooms();

            CreateRoomsWithPrices(initializationRooms, countOfRooms, pricesPerNightRooms);
        }

        // Create rooms with prices per night for each type of room in the hotel
        // and initialize the list of rooms for each type of room 
        // with the number of rooms and the price per night ...
        private void CreateRoomsWithPrices(IInitializationRooms initializationRooms, int[] countOfRooms, decimal[] pricesPerNightRoom)
        {
            if(countOfRooms.Length > 3)
            {
                throw new ArgumentException("The hotel currently offers a maximum of 3 types of rooms");
            }

            if (pricesPerNightRoom.Length > 3)
            {
                throw new ArgumentException("The hotel currently offers a maximum of 3 types of rooms");
            }

            if(countOfRooms.Length != pricesPerNightRoom.Length)
            {
                throw new ArgumentException("The number of rooms and prices per night must be the same");
            }
            else
            {
                for (int i = 0; i < countOfRooms.Length; i++)
                {
                    if (countOfRooms[i] < 0)
                    {
                        throw new ArgumentException("The number of rooms must be greater than 0");
                    } 
                    else if(countOfRooms[i] > 0)
                    {

                        if(i == 0)
                        {
                            PricePerNightStandardRoom = pricesPerNightRoom[i];
                            // Initialize the list of standard rooms with the number of rooms and the price per night
                            _standardRooms = new List<Room>();
                            initializationRooms.ExecuteInitializeRoom(_overviewStandardRoomsString, _standardRooms, 
                                    RoomType.Standard, countOfRooms[i], PricePerNightStandardRoom);
                        } 
                        else if(i == 1)
                        {
                            PricePerNightDeluxeRoom = pricesPerNightRoom[i];
                            // Initialize the list of standard rooms with the number of rooms and the price per night
                            _deluxeRooms = new List<Room>();
                            initializationRooms.ExecuteInitializeRoom(_overviewDeluxeRoomsString, _deluxeRooms, 
                                    RoomType.Deluxe, countOfRooms[i], PricePerNightDeluxeRoom);
                        }
                        else if(i == 2)
                        {
                            PricePerNightSuiteRoom = pricesPerNightRoom[i];
                            // Initialize the list of standard rooms with the number of rooms and the price per night
                            _suiteRooms = new List<Room>();
                            initializationRooms.ExecuteInitializeRoom(_overviewSuiteRoomsString, _suiteRooms,
                                    RoomType.Suite, countOfRooms[i], PricePerNightSuiteRoom);
                        }
                    }
                }
            }
        }

        // Create a new reservation asynchronously and add it to the reservation book
        public async Task CreateReservationAsync(Reservation reservation)
        {
            await _reservationBook.MakeReservationAsync(reservation);
        }

        // Get all reservations asynchronously from the reservation book
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
         => await _reservationBook.GetAllReservationsAsync();

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
