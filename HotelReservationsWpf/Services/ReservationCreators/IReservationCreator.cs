using HotelReservationsWpf.Models;

namespace HotelReservationsWpf.Services.ReservationCreators
{
    public interface IReservationCreator
    {
        Task CreateReservationAsync(Reservation reservation);
    }
}
