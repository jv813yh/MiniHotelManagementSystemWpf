using HotelReservationsWpf.Exceptions;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace HotelReservationsWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Hotel _hotel;
        private readonly int[] _countOfRooms = [12, 6, 3];
        private readonly decimal[] _pricesPerNightRoom = [50, 78, 110];

        public App()
        {
            _hotel = new Hotel("Your Paradise", _countOfRooms, _pricesPerNightRoom);
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_hotel)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
