using HotelReservationsWpf.Models;

namespace HotelReservationsWpf.Services.ReservationRemovers
{
    public interface IReservationRemover
    {
        Task<(bool, RoomType)> RemoveReservationAsync(int roomNumber, string guestName);
    }
}
