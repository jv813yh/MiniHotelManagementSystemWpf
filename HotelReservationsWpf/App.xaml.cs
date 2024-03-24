﻿using HotelReservationsWpf.DbContexts;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Services.ReservationCreators;
using HotelReservationsWpf.Services.ReservationProviders;
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
        private static readonly string _connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hotelManagement.db")}";


        HotelManagementDbContextFactory _dbHotelContextFactory;

        // Services for connecting to the database based on connectionstring
        // and performing work with reservations such as creation, loading, deletion
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;

        // Store for saving the current view model of the application
        private NavigationStore _navigationStore;

        // Fields
        private readonly Hotel _hotel;
        private readonly int[] _countOfRooms = [12, 6, 3];
        private readonly decimal[] _pricesPerNightRoom = [50, 78, 110];


        public App()
        {
            //
            _dbHotelContextFactory = new HotelManagementDbContextFactory(_connectionString);

            //
            _reservationProvider = DatabaseReservationProvider.CreateDatabaseReservationProvider(_dbHotelContextFactory, _pricesPerNightRoom, _countOfRooms.Length);
            _reservationCreator = new DatabaseReservationCreator(_dbHotelContextFactory);

            // 
            _hotel = new Hotel("Your Paradise", _countOfRooms, _pricesPerNightRoom,
                                    _reservationCreator, _reservationProvider);

            // 
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Migration will be created if it does not exist or is out of date
            using (HotelManagementDbContext dbContext = _dbHotelContextFactory.CreateHotelManagementDbContext())
            {
                dbContext.Database.Migrate();
            }

            // Set the current view model of the application
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
            return ReservationsListingViewModel.CreateReservationsListingViewModel(_hotel, new NavigationServiceWpf(_navigationStore, CreateMakeReservationViewModel), 
                                                            _reservationProvider);
        }
    }
}
