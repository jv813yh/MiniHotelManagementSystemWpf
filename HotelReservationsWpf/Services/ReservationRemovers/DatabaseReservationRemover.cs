using HotelReservationsWpf.DbContexts;
using HotelReservationsWpf.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace HotelReservationsWpf.Services.ReservationRemovers
{
    public class DatabaseReservationRemover : IReservationRemover   
    {
        // Factory class to create instances of the HotelManagementDbContext class
        private readonly HotelManagementDbContextFactory _dbContextFactory;

        public DatabaseReservationRemover(HotelManagementDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // Remove a reservation from the database by room number and guest name
        public async Task<(bool, RoomType, decimal)> RemoveReservationAsync(int roomNumber, string guestName)
        {
            bool canExecute = true;
            decimal totalCost = 0;
            RoomType roomType = RoomType.Unused;

            if(roomNumber <= 0)
            {
                MessageBox.Show("Room number must be greater than 0", "Information", 
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                canExecute = false;
            }

            if(string.IsNullOrEmpty(guestName))
            {
                MessageBox.Show("Guest name must not be empty", "Information",
                                       MessageBoxButton.OK, MessageBoxImage.Information);
                canExecute = false;
            }

            /*
            else if(IsNoValidName(guestName))
            {
                MessageBox.Show("No valid name", "Information",
                                      MessageBoxButton.OK, MessageBoxImage.Information);
                canExecute = false;
            }
            */

            if(canExecute)
            {
                using (HotelManagementDbContext dbContext = _dbContextFactory.CreateHotelManagementDbContext())
                {
                    var reservation = await dbContext.Reservations
                        .Where(r => r.RoomNumber == roomNumber && r.GuestName == guestName)
                        .FirstOrDefaultAsync();

                    if (reservation == null)
                    {
                        //throw new InvalidOperationException($"Reservation for room number {roomNumber} and guest name {guestName} does not exist");

                        MessageBox.Show($"Reservation for room number {roomNumber} and guest name {guestName} does not exist", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);

                        canExecute = false;
                    }
                    else
                    {
                        totalCost = reservation.TotalCost;
                        roomType = reservation.RoomTypeDTO;

                        // If the reservation exists, remove it from the database and save changes
                        dbContext.Reservations.Remove(reservation);

                        await dbContext.SaveChangesAsync();
                    }
                }
            }

            return (canExecute, roomType, totalCost);
        }
    }
}
