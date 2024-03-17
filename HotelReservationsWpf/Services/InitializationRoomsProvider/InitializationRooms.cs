using HotelReservationsWpf.Models;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace HotelReservationsWpf.Services.InitializationRoomsProvider
{
    public class InitializationRooms : IInitializationRooms
    {
        public void ExecuteInitializeRoom(string connectionString, List<Room> roomsList, 
                        RoomType roomType, int countOfRooms, decimal pricePerNight)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (roomsList == null)
            {
                throw new ArgumentNullException(nameof(roomsList));
            }

            // If the file exists, read the data from the file and deserialize it
            if (File.Exists(connectionString))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(roomsList.GetType());

                    using (StreamReader sr = new StreamReader(connectionString))
                    {
                        roomsList = (List<Room>)serializer.Deserialize(sr);
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
                // Create a new room object and add it to the list according to the room type
                // and the price per night
                InsertListWithRoomsSwitch(roomType, countOfRooms, pricePerNight, roomsList);
            }
        }

        // Create a new room object and add it to the list according to the room type
        // and the price per night
        private void InsertListWithRooms(int countOfRooms, RoomType roomType, decimal price, List<Room> roomsList)
        {
            for(int i = 0; i < countOfRooms; i++)
            {
                Room room = new Room(i + 1, roomType, RoomStatus.Available, price);
                roomsList.Add(room);
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
