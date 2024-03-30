using HotelReservationsWpf.Models;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace HotelReservationsWpf.Services.SaveRoomsProviders
{
    public class SaveRooms : ISaveRoomsProvider
    {
        public void ExecuteSaveRoomToXml(string connectionString, List<Room> roomsList)
        {
            bool canExecute = true;

            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("The connection string is empty", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                canExecute = false;
            }

            if (roomsList == null)
            {
                MessageBox.Show("The list of rooms is null", "Error",
                                       MessageBoxButton.OK, MessageBoxImage.Error);
                canExecute = false;
            } 
            else if(roomsList.GetType() != typeof(List<Room>))
            {
                MessageBox.Show("The list of rooms is not of type List<Room>", "Error",
                                                          MessageBoxButton.OK, MessageBoxImage.Error);
                canExecute = false;
            }

            if(canExecute)
            {
                try
                {
                    // Serialize the list of rooms and write it to the file

                    XmlSerializer serializer = new XmlSerializer(roomsList.GetType());

                    using (StreamWriter sw = new StreamWriter(connectionString))
                    {
                        serializer.Serialize(sw, roomsList);
                    }

                }
                catch (InvalidOperationException)
                {
                    throw new InvalidOperationException($"The file {connectionString} is not in the correct format");
                }
                catch (Exception)
                {
                    throw new Exception($"The file {connectionString} could not be written with current status of rooms");
                }

            }
        }
    }
}
