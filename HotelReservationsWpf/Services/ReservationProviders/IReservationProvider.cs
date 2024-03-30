using HotelReservationsWpf.Models;

namespace HotelReservationsWpf.Services.ReservationProviders
{
    public interface IReservationProvider
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync(bool isReadOnly);
        void CheckSettings();
    }
}
