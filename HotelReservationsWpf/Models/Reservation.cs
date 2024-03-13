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
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        public TimeSpan NumberOfDaysRemaining 
            => CheckOutDate.Subtract(CheckInDate);

        public Reservation(Room room, GuestPerson guestName, DateTime startDate, DateTime endDate, int numberOfDays)
        {
            CurrentRoom = room;
            GuestName = guestName;
            CheckInDate = startDate;
            CheckOutDate = endDate;
            TotalCost = room.CostPerNight * numberOfDays;
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
