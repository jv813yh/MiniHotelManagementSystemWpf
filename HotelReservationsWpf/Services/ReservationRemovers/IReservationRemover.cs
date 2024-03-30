namespace HotelReservationsWpf.Services.ReservationRemovers
{
    public interface IReservationRemover
    {
        Task<bool> RemoveReservationAsync(int roomNumber, string guestName);
    }
}
