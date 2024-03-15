using HotelReservationsWpf.Models;
using System.IO;
using System.Xml.Serialization;

namespace HotelReservationsWpf.Services.InitializationRoomsProvider
{
    public class InitializationRooms : IInitializationRooms
    {
        private string _overviewRoomsString = "overviewOfRoomsFile.xml";

        private List<Room> _returnRooms;

        private decimal _pricePerNightStandardRoom, 
                        _pricePerNightDeluxeRoom, 
                        _pricePerNightSuiteRoom;

        public InitializationRooms(decimal pricePerNightStandardRoom, decimal pricePerNightDeluxeRoom, decimal pricePerNightSuiteRoom)
        {
            _returnRooms = new List<Room>();

            _pricePerNightStandardRoom = pricePerNightStandardRoom;
            _pricePerNightDeluxeRoom = pricePerNightDeluxeRoom;
            _pricePerNightSuiteRoom = pricePerNightSuiteRoom;
        }

        public List<Room> GetRooms()
        {

            if (File.Exists(_overviewRoomsString))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(_returnRooms.GetType());

                    using (StreamReader sr = new StreamReader(_overviewRoomsString))
                    {
                        _returnRooms = (List<Room>)serializer.Deserialize(sr);
                    }
                }
                catch (InvalidOperationException)
                {
                    throw new InvalidOperationException($"The file {_overviewRoomsString} is not in the correct format");
                }
                catch (Exception)
                {
                    throw new Exception($"The file {_overviewRoomsString} could not be read");
                }
            }
            else
            {
                // Create a new room object and add it to the list according to the room type
                // and the price per night
                InsertListWithRooms(0, 12, RoomType.Standard, _pricePerNightStandardRoom);
                InsertListWithRooms(13, 20, RoomType.Deluxe, _pricePerNightDeluxeRoom);
                InsertListWithRooms(21, 25, RoomType.Suite, _pricePerNightSuiteRoom);
            }

            return _returnRooms;
        }

        // Create a new room object and add it to the list according to the room type
        // and the price per night
        private void InsertListWithRooms(int startIndex, int endIndex, RoomType roomType, decimal price)
        {
            for(int i = startIndex; i <= endIndex; i++)
            {
                Room room = new Room(i, roomType, RoomStatus.Available, price);
                _returnRooms.Add(room);
            }
        }
    }
}
