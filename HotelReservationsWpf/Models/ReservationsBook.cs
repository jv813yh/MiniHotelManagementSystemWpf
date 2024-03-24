using HotelReservationsWpf.Exceptions;
using HotelReservationsWpf.Services.ReservationCreators;
using HotelReservationsWpf.Services.ReservationProviders;

namespace HotelReservationsWpf.Models
{
    // Class to manage reservations in the hotel (creating, getting, deleting reservations)
    public class ReservationsBook
    {
        // Services for loading reservations from the database and creating a reservations
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationProvider _reservationProvider;

        public ReservationsBook(IReservationCreator reservationCreator, IReservationProvider reservationProvider)
        {
            _reservationCreator = reservationCreator;
            _reservationProvider = reservationProvider;
        }

        /// <summary>
        /// Async method to create a new reservation and check for conflicts
        /// </summary>
        /// <param name="newReservation"></param>
        /// <exception cref="ConflictReservationsException"></exception>
        public async Task MakeReservationAsync(Reservation newReservation)
        {
            
            // Check if there is a conflict with the new reservation
            foreach (var reservation in await _reservationProvider.GetAllReservationsAsync())
            {
                if (reservation.IsConflict(newReservation))
                {
                    throw new ConflictReservationsException("There is a conflict with the reservation", newReservation, reservation);
                }
            }
            
            // Change the room status to Occupied
            newReservation.CurrentRoom.RoomStatus = RoomStatus.Occupied;

            // Create a new reservation in the database async
            await _reservationCreator.CreateReservationAsync(newReservation);
        }

        /// <summary>
        /// Async method to get all reservations 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
            => await _reservationProvider.GetAllReservationsAsync();
    }
}
