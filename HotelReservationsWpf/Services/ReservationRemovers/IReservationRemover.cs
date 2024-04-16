using HotelReservationsWpf.Models;

namespace HotelReservationsWpf.Services.ReservationRemovers
{
    public interface IReservationRemover
    {
        Task<(bool, RoomType, decimal)> RemoveReservationAsync(int roomNumber, string guestName);
    }
}
