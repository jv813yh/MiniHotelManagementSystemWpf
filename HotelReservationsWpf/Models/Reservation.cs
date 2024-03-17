using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationsWpf.Models
{
    public class Reservation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public Guid ReservationId { get; set; }

        [Required]
        public Room CurrentRoom { get; set; }

        [Required]
        public GuestPerson GuestName { get; set; }

        [Required]
        public DateOnly CheckInDate { get; set; }

        [Required]
        public DateOnly CheckOutDate { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

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
