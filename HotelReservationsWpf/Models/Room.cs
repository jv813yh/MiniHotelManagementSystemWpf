using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservationsWpf.Models
{
    // 1-12 - Standard
    // 13-20 - Deluxe
    // 21-25 - Suite
    // RoomType and RoomStatus are enums that define the type and status of a room
    public enum RoomType
    {
        Standard,
        Deluxe,
        Suite
    }
    public enum RoomStatus
    {
        Available,
        Occupied,
    }
    // Room class defines the properties of a room.
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public Guid RoomId { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        [Required]
        public RoomStatus RoomStatus { get; set; }

        // Price for one night stay in the room
        [Required]
        public decimal CostPerNight { get; set; }

        public Room(int roomNumber, RoomType roomType, RoomStatus roomStatus, decimal price)
        {
            RoomNumber = roomNumber;
            RoomType = roomType;
            RoomStatus = roomStatus;
            CostPerNight = price;

        }

        // Override the Equals method to compare two Room objects based on their RoomNumber and RoomType
        public override bool Equals(object? obj)
            => (obj is Room room) && (RoomNumber == room.RoomNumber)
            && (RoomType == room.RoomType);

        // Override the GetHashCode method to return the hash code of the RoomNumber and RoomType
        public override int GetHashCode()
            => HashCode.Combine(RoomNumber, RoomType);
        
        // Override the ToString method to return a string representation of the Room object
        public override string ToString()
        {
            return $"Room {RoomNumber} - {RoomType} - {RoomStatus} - {CostPerNight:0.00} €";
        }
    }
}
