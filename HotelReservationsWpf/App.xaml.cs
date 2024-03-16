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

        public App()
        {
            _hotel = new Hotel("Hotel Your Paradise", 50, 78, 110);
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
