using HotelReservationsWpf.Models;

namespace HotelReservationsWpf.Services.SaveRoomsProviders
{
    public interface ISaveRoomsProvider
    {
        void ExecuteSaveRoomToXml(string connectionString, List<Room> roomsList);
    }
}
