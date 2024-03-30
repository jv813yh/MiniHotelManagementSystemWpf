using HotelReservationsWpf.DbContexts;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Services.InitializationRoomsProviders;
using HotelReservationsWpf.Services.ReservationCreators;
using HotelReservationsWpf.Services.ReservationProviders;
using HotelReservationsWpf.Services.ReservationRemovers;
using HotelReservationsWpf.Services.SaveRoomsProviders;
using HotelReservationsWpf.Stores;
using HotelReservationsWpf.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Windows;

namespace HotelReservationsWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Connection string to the database (Sqlite)
        private static readonly string _connectionString = 
            $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hotelManagement.db")}";

        // Factory for creating a database context
        HotelManagementDbContextFactory _dbHotelContextFactory;

        // Services for connecting to the database based on connectionstring
        // and performing work with reservations such as creation, loading, deletion
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationRemover _reservationRemover;

        // Services for initializing rooms and saving rooms
        private readonly IInitializationRooms _initializationRooms;
        private readonly ISaveRoomsProvider _saveRoomsProvider;

        // Store for saving the current view model of the application
        private NavigationStore _navigationStore;

        // Hotel is centralized an managed in a single location - HotelStore
        private readonly HotelStore _hotelStore;

        // Fields
        private readonly Hotel _hotel;
        private readonly int[] _countOfRooms = [12, 6, 3];
        private readonly decimal[] _pricesPerNightRoom = [50, 78, 110];

        public App()
        {
            //
            _dbHotelContextFactory = new HotelManagementDbContextFactory(_connectionString);

            //
            _reservationProvider = DatabaseReservationProvider.CreateDatabaseReservationProvider(_dbHotelContextFactory, 
                     _pricesPerNightRoom, _countOfRooms.Length);

            _reservationCreator = new DatabaseReservationCreator(_dbHotelContextFactory);
            _reservationRemover  = new DatabaseReservationRemover(_dbHotelContextFactory);


            _initializationRooms = new InitializationRooms();
            _saveRoomsProvider = new SaveRooms();

            // 
            _hotel = new Hotel("Your Paradise", _countOfRooms, _pricesPerNightRoom,
                                    _reservationCreator, _reservationProvider, _reservationRemover,
                                    _initializationRooms, _saveRoomsProvider);

            _hotelStore = new HotelStore(_hotel);

            // 
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Migration will be created if it does not exist or is out of date
            using (HotelManagementDbContext dbContext = _dbHotelContextFactory.CreateHotelManagementDbContext())
            {
                // We can use PMC commands to create the database and apply migrations
                // but I think for simple and small projects, we can also do it this way
                dbContext.Database.EnsureCreated();
                dbContext.Database.Migrate();
            }

            // Set the current view model of the initial application
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
            return new EntranceToHotelViewModel(_hotelStore,
                         new NavigationServiceWpf(_navigationStore, CreateMakeReservationViewModel));
        }
        
        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotelStore,    
                            new NavigationServiceWpf(_navigationStore, CreateReservationsListingViewModel),
                            new NavigationServiceWpf(_navigationStore, CreateOverviewViewModel));
        }

        private ReservationsListingViewModel CreateReservationsListingViewModel()
        {
            return ReservationsListingViewModel.ReservationsListingViewModelBuilder(_hotelStore, 
                       new NavigationServiceWpf(_navigationStore, CreateMakeReservationViewModel),
                       new NavigationServiceWpf(_navigationStore, CreateOverviewViewModel));
        }

        private OverviewViewModel CreateOverviewViewModel()
        {
            return new OverviewViewModel(_hotelStore, new NavigationServiceWpf(_navigationStore, CreateMakeReservationViewModel), 
                            new NavigationServiceWpf(_navigationStore, CreateReservationsListingViewModel));
        }
    }
}
