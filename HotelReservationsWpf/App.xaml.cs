using HotelReservationsWpf.Exceptions;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Stores;
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
        private NavigationStore _navigationStore;

        private readonly Hotel _hotel;
        private readonly int[] _countOfRooms = [12, 6, 3];
        private readonly decimal[] _pricesPerNightRoom = [50, 78, 110];


        public App()
        {
            _navigationStore = new NavigationStore();

            _hotel = new Hotel("Your Paradise", _countOfRooms, _pricesPerNightRoom);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateEntranceToHotelViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_hotel, _navigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        private EntranceToHotelViewModel CreateEntranceToHotelViewModel()
        {
            return new EntranceToHotelViewModel(_hotel, new NavigationServiceWpf(_navigationStore, CreateMakeReservationViewModel));
        }
        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotel, new NavigationServiceWpf(_navigationStore, CreateReservationsListingViewModel));
        }

        private ReservationsListingViewModel CreateReservationsListingViewModel()
        {
            return ReservationsListingViewModel.CreateReservationsListingViewModel(_hotel, new NavigationServiceWpf(_navigationStore, CreateMakeReservationViewModel));
        }
    }

}
