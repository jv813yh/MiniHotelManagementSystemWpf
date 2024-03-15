using HotelReservationsWpf.Exceptions;

namespace HotelReservationsWpf.Models
{
    public class ReservationsBook
    {
        public List<Reservation> Reservations { get; set; }

        public ReservationsBook()
        {
            Reservations = new List<Reservation>();
        }

        /// <summary>
        /// Method to create a new reservation and check for conflicts
        /// </summary>
        /// <param name="newReservation"></param>
        /// <exception cref="ConflictReservationsException"></exception>
        public void MakeReservation(Reservation newReservation)
        {
            // Check if there is a conflict with the new reservation
            if (Reservations.Any(r => r.IsConflict(newReservation)))
            {
                throw new ConflictReservationsException("There is a conflict with the reservation", 
                    newReservation, Reservations.First(r => r.IsConflict(newReservation)));
            }

            // Add the new reservation 
            newReservation.CurrentRoom.RoomStatus = RoomStatus.Occupied;
            Reservations.Add(newReservation);
        }

        /// <summary>
        /// Method to get all reservations 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Reservation> GetAllReservations()
            => Reservations;

    }
}
