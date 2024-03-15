using HotelReservationsWpf.Models;

namespace HotelReservationsWpf.Services.InitializationRoomsProvider
{
    public interface IInitializationRooms
    { 
        List<Room> GetRooms();
    }
}
