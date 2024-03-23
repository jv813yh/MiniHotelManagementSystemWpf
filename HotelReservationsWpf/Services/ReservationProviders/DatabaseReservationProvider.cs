using HotelReservationsWpf.DbContexts;
using HotelReservationsWpf.DTOs;
using HotelReservationsWpf.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationsWpf.Services.ReservationProviders
{
    public class DatabaseReservationProvider : IReservationProvider
    {
        // Fields
        private readonly HotelManagementDbContextFactory _dbContextFactory;
        private readonly Hotel _hotel;

        public DatabaseReservationProvider(HotelManagementDbContextFactory hotelManagementDbContextFactory, Hotel hotel)
        {
            _dbContextFactory = hotelManagementDbContextFactory;
            _hotel = hotel;
        }

        // Get all reservations from the database asynchronously and return them as a collection of Reservation objects
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            // Create a new instance of the HotelManagementDbContext class 
            using (HotelManagementDbContext dbContext = _dbContextFactory.CreateHotelManagementDbContext())
            {
                // Get all reservations from the database 
                IEnumerable<ReservationDTO> reservationsDto = await dbContext.Reservations.ToListAsync();

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
            switch (reservationDto.RoomType)
            {
                case RoomType.Standard:
                    pricePerNight = _hotel.GetPriceForStandardRoom();
                    break;
                case RoomType.Deluxe:
                    pricePerNight = _hotel.GetPriceForDeluxeRoom();
                    break;
                case RoomType.Suite:
                    pricePerNight = _hotel.GetPriceForSuiteRoom();
                    break;
                default:
                    break;

            }

            // Create a new instance of the Reservation class with the Room, GuestPerson, CheckInDate, and CheckOutDate properties
            return new Reservation(new Room(reservationDto.RoomNumber, reservationDto.RoomType, RoomStatus.Occupied, pricePerNight),
                                        new GuestPerson(reservationDto.GuestName, reservationDto.GuestEmail, reservationDto.PhoneNumber), 
                                                reservationDto.CheckInDate, reservationDto.CheckOutDate);
        }
    }
}
