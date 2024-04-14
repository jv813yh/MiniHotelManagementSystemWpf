using HotelReservationsWpf.DbContexts;
using HotelReservationsWpf.DTOs;
using HotelReservationsWpf.Models;

namespace HotelReservationsWpf.Services.ReservationCreators
{
    public class DatabaseReservationCreator : IReservationCreator
    {
        private readonly HotelManagementDbContextFactory _dbContextFactory;

        public DatabaseReservationCreator(HotelManagementDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // Create reservation in the database async method 
        public async Task CreateReservationAsync(Reservation reservation)
        {
            // Create a new instance of the HotelManagementDbContext class 
            using(HotelManagementDbContext context = _dbContextFactory.CreateHotelManagementDbContext())
            {
                // Mapping Reservation to ReservationDto
                ReservationDTO newReservationDto = MappingReservationToReservationDto(reservation);

                // Add new reservation to the database
                context.Reservations.Add(newReservationDto);

                // Save changes to the database 
                await context.SaveChangesAsync();
            }
        }

        // Mapping Reservation to ReservationDto
        // better choice to use AutoMapper
        private ReservationDTO MappingReservationToReservationDto(Reservation reservation)
        {
            // Check if reservation is null
            if (reservation == null)
            {
                throw new NullReferenceException("You try to map empty reservation");
            }

            // 
            return new ReservationDTO
            {
                RoomNumber = reservation.CurrentRoom.RoomNumber,
                RoomTypeDTO = reservation.CurrentRoom.RoomType,
                GuestName = reservation.GuestName.GuestName,
                GuestEmail = reservation.GuestName.Email,
                PhoneNumber = reservation.GuestName.PhoneNumber,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                TotalCost = reservation.TotalCost
            };
        }
    }
}
