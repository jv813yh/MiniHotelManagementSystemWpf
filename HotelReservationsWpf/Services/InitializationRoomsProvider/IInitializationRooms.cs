using HotelReservationsWpf.Models;

namespace HotelReservationsWpf.Services.InitializationRoomsProvider
{
    public interface IInitializationRooms
    {
        void ExecuteInitializeRoom(string connectionString, List<Room> roomsList,
                RoomType roomType, int countOfRooms, decimal pricePerNight);
    }
}
