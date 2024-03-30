using HotelReservationsWpf.Models;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace HotelReservationsWpf.Services.InitializationRoomsProviders
{
    public class InitializationRooms : IInitializationRooms
    {
        public List<Room> ExecuteInitializeRoomFromXml(string connectionString,
                        RoomType roomType, int countOfRooms, decimal pricePerNight)
        {
            List<Room> returnRoomsList;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            // If the file exists, read the data from the file and deserialize it
            if (File.Exists(connectionString))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Room>));

                    using (StreamReader sr = new StreamReader(connectionString))
                    {
                        returnRoomsList = (List<Room>)serializer.Deserialize(sr);
                    }

                }
                catch (InvalidOperationException)
                {
                    throw new InvalidOperationException($"The file {connectionString} is not in the correct format");
                }
                catch (Exception)
                {
                    throw new Exception($"The file {connectionString} could not be read");
                }
            }
            else
            {
                returnRoomsList = new List<Room>();
                // Create a new room object and add it to the list according to the room type
                // and the price per night
                InsertListWithRoomsSwitch(roomType, countOfRooms, pricePerNight, returnRoomsList);
            }


            if (returnRoomsList == null)
            {
                throw new ArgumentNullException(nameof(returnRoomsList));
            }

            return returnRoomsList;
        }

        // Create a new room object and add it to the list according to the room type
        // and the price per night
        private void InsertListWithRooms(int countOfRooms, RoomType roomType, decimal price, List<Room> roomsList)
        {
            for(int i = 0; i < countOfRooms; i++)
            {
                roomsList.Add(new Room(i + 1, roomType, RoomStatus.Available, price));
            }
        }

        private void InsertListWithRoomsSwitch(RoomType roomType,
                 int countOfRooms, decimal pricePerNight, List<Room> roomsList)
        {
            switch (roomType)
            {
                case RoomType.Standard:
                    InsertListWithRooms(countOfRooms, RoomType.Standard, pricePerNight, roomsList);
                    break;
                case RoomType.Deluxe:
                    InsertListWithRooms(countOfRooms, RoomType.Deluxe, pricePerNight, roomsList);
                    break;
                case RoomType.Suite:
                    InsertListWithRooms(countOfRooms, RoomType.Suite, pricePerNight, roomsList);
                    break;
                default:
                    MessageBox.Show("Invalid room type");
                    break;
            }
        }
    }
}
