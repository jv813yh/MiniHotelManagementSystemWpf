using HotelReservationsWpf.DbContexts;
using HotelReservationsWpf.DTOs;
using HotelReservationsWpf.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationsWpf.Services.ReservationProviders
{
    public class DatabaseReservationProvider : IReservationProvider
    {
        // Factory class to create instances of the HotelManagementDbContext class
        private readonly HotelManagementDbContextFactory _dbContextFactory;

        // Prices per night for the rooms
        private readonly decimal[] _pricesPerNightRoom;

        // Number of room types
        private readonly int _numberOfRoomTypes;

        public DatabaseReservationProvider(HotelManagementDbContextFactory hotelManagementDbContextFactory,
                       decimal[] pricesPerNightRoom, int numberOfRoomTypes)
        {
            _dbContextFactory = hotelManagementDbContextFactory;
            _pricesPerNightRoom = pricesPerNightRoom;
            _numberOfRoomTypes = numberOfRoomTypes;
        }

        public static DatabaseReservationProvider CreateDatabaseReservationProvider(HotelManagementDbContextFactory hotelManagementDbContextFactory,
                                  decimal[] pricesPerNightRoom, int numberOfRoomTypes)
        {
            DatabaseReservationProvider returnReservationProvider = 
                new DatabaseReservationProvider(hotelManagementDbContextFactory, pricesPerNightRoom, numberOfRoomTypes);

            returnReservationProvider.CheckSettings();

            return returnReservationProvider;
        }

        // Get all reservations from the database asynchronously and return them as a read only collection of Reservation objects
        // with no tracking or as a collection of Reservation objects
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync(bool isReadOnly)
        {
            // Create a new instance of the HotelManagementDbContext class 
            using (HotelManagementDbContext dbContext = _dbContextFactory.CreateHotelManagementDbContext())
            {
                IEnumerable<ReservationDTO>? reservationsDto = null;

                if(isReadOnly)
                {
                    // Get all reservations from the database async with no tracking
                    reservationsDto = await dbContext.Reservations.AsNoTracking().ToListAsync();
                }
                else
                {
                    // Get all reservations from the database async
                    reservationsDto = await dbContext.Reservations.ToListAsync();
                }

                // Mapping ReservationDTO to Reservation and return them as a collection of Reservation objects
                return reservationsDto.Select(reservationDto => MappingReservationDtoToReservation(reservationDto));
            }
        }

        // Mapping ReservationDTO to Reservation
        // better choice to use AutoMapper
        private Reservation MappingReservationDtoToReservation(ReservationDTO reservationDto)
        {
            decimal pricePerNight = 0;

            // Determine the price per night based on the RoomType
            switch (reservationDto.RoomTypeDTO)
            {
                case RoomType.Standard:
                    pricePerNight = _pricesPerNightRoom[0];
                    break;
                case RoomType.Deluxe:
                    pricePerNight = _pricesPerNightRoom[1];
                    break;
                case RoomType.Suite:
                    pricePerNight = _pricesPerNightRoom[2];
                    break;
                default:
                    break;

            }

            // Create a new instance of the Reservation class with the Room, GuestPerson, CheckInDate, and CheckOutDate properties
            return new Reservation(new Room(reservationDto.RoomNumber, reservationDto.RoomTypeDTO, RoomStatus.Occupied, pricePerNight),
                                        new GuestPerson(reservationDto.GuestName, reservationDto.GuestEmail, reservationDto.PhoneNumber), 
                                                reservationDto.CheckInDate, reservationDto.CheckOutDate);
        }

        public void CheckSettings()
        {
            if (_pricesPerNightRoom.Length != _numberOfRoomTypes)
            {
                throw new ArgumentException("The prices per night for the rooms and number of room types are not set correctly");
            }
        }
    }
}
