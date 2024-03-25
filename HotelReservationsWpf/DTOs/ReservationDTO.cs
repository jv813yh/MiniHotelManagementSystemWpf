using HotelReservationsWpf.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationsWpf.DTOs
{
    public class ReservationDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public Guid Id { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public RoomType RoomTypeProperty { get; set; }

        [Required]
        public string GuestName { get; set; } = string.Empty;

        [Required]
        public string GuestEmail { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public DateOnly CheckInDate { get; set; }

        [Required]
        public DateOnly CheckOutDate { get; set; }

        [Required]
        public decimal TotalCost { get; set; }
    }
}
