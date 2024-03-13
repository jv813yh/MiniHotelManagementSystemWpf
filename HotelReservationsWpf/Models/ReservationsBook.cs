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

        // Method to add a new reservation 
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

        // Method to get all reservations 
        public IEnumerable<Reservation> GetAllReservations()
            => Reservations;

    }
}
