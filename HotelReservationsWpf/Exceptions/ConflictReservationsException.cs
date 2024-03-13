using HotelReservationsWpf.Models;

namespace HotelReservationsWpf.Exceptions
{
    // Exception class to handle conflicts in reservations when adding a new reservation
    public class ConflictReservationsException : Exception
    {
        public Reservation NewReservation { get; }
        public Reservation ExistingReservation { get; }

        public ConflictReservationsException(Reservation newReservation, Reservation existingReservation)
        {
            NewReservation = newReservation;
            ExistingReservation = existingReservation;
        }

        public ConflictReservationsException(string message, Reservation newReservation, Reservation existingReservation) : base(message)
        {
            NewReservation = newReservation;
            ExistingReservation = existingReservation;
        }

        public ConflictReservationsException(string message, Exception innerException, Reservation newReservation, Reservation existingReservation) : base(message, innerException)
        {
            NewReservation = newReservation;
            ExistingReservation = existingReservation;
        }
    }
}
