using HotelReservationsWpf.Models;

namespace HotelReservationsWpf.Services.InitializationRoomsProviders
{
    public interface IInitializationRooms
    {
        List<Room> ExecuteInitializeRoomFromXml(string connectionString,
                        RoomType roomType, int countOfRooms, decimal pricePerNight);
    }
}
