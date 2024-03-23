namespace HotelReservationsWpf.Models
{
    public class Reservation
    {
        public Room CurrentRoom { get; }

        public GuestPerson GuestName { get; }

        public DateOnly CheckInDate { get; }

        public DateOnly CheckOutDate { get; }

        public decimal TotalCost { get; }

        public int NumberOfDaysRemaining 
            => CheckOutDate.DayOfYear - CheckInDate.DayOfYear;

        public Reservation(Room room, GuestPerson guestName, DateOnly startDate, DateOnly endDate)
        {
            CurrentRoom = room;
            GuestName = guestName;
            CheckInDate = startDate;
            CheckOutDate = endDate;
            TotalCost = CurrentRoom.CostPerNight * NumberOfDaysRemaining;
        }

        public bool IsConflict(Reservation newReservation)
        {
            if(!CurrentRoom.Equals(newReservation.CurrentRoom))
            {
                return false;
            }

            return (CheckInDate < newReservation.CheckOutDate) && (CheckOutDate > newReservation.CheckInDate);
        }
    }
}
