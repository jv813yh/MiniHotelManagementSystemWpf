using HotelReservationsWpf.DbContexts;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Services.EarningsReadingProvider;
using HotelReservationsWpf.Services.EarningsWritingProvider;
using HotelReservationsWpf.Services.InitializationRoomsProviders;
using HotelReservationsWpf.Services.ReservationCreators;
using HotelReservationsWpf.Services.ReservationProviders;
using HotelReservationsWpf.Services.ReservationRemovers;
using HotelReservationsWpf.Services.SaveRoomsProviders;
using HotelReservationsWpf.Stores;
using HotelReservationsWpf.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

namespace HotelReservationsWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    { 
        // Fields
        //private readonly Hotel _hotel;
        private readonly int[] _countOfRooms = [12, 6, 3];
        private readonly decimal[] _pricesPerNightRoom = [50, 78, 110];

        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext,services) =>
                {
                    string hotelName = hostContext.Configuration.GetValue<string>("HotelName");
                    string connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");
                    // Factory for creating a database context
                    services.AddSingleton(new HotelManagementDbContextFactory(connectionString));

                    // Services for connecting to the database based on connectionstring
                    // and performing work with reservations such as creation, loading, deletion
                    services.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
                    services.AddSingleton<IReservationRemover, DatabaseReservationRemover>();
                    services.AddSingleton<IReservationProvider>(sp =>
                    {
                        var dbContextFactory = sp.GetRequiredService<HotelManagementDbContextFactory>();
                        return DatabaseReservationProvider.CreateDatabaseReservationProvider(dbContextFactory, _pricesPerNightRoom, _countOfRooms.Length);
                    });

                    services.AddSingleton<NavigationStore>();

                    // Services for initializing rooms and saving rooms
                    services.AddSingleton<IInitializationRooms, InitializationRooms>();
                    services.AddSingleton<ISaveRoomsProvider, SaveRooms>();

                    // Services for writing earnings to an Excel file and reading from excel
                    services.AddSingleton<IEarningsWritting, ExcelEarningsWriting>();
                    services.AddSingleton<IEarningReading, ExcelEarningReading>();

                    //
                    services.AddTransient((sp) => CreateEntranceToHotelViewModelInService(sp));
                    services.AddSingleton<NavigationServiceWpf<EntranceToHotelViewModel>>();
                    services.AddSingleton<Func<EntranceToHotelViewModel>>(sp => () => sp.GetRequiredService<EntranceToHotelViewModel>());

                    //
                    services.AddTransient<MakeReservationViewModel>();
                    services.AddSingleton<Func<MakeReservationViewModel>> ((sp) => () => sp.GetRequiredService<MakeReservationViewModel>());
                    services.AddSingleton<NavigationServiceWpf<MakeReservationViewModel>>();

                    //
                    services.AddTransient<OverviewViewModel>();
                    services.AddSingleton<Func<OverviewViewModel>>((sp) => () => sp.GetRequiredService<OverviewViewModel>());
                    services.AddSingleton<NavigationServiceWpf<OverviewViewModel>>();

                    //
                    services.AddTransient((sp) => CreateReservationsListingViewModelInService(sp));
                    services.AddSingleton<Func<ReservationsListingViewModel>>((sp) => () => sp.GetRequiredService<ReservationsListingViewModel>());
                    services.AddSingleton<NavigationServiceWpf<ReservationsListingViewModel>>();



                    services.AddSingleton(sp =>
                    {
                        return new Hotel(hotelName,
                            _countOfRooms,
                            _pricesPerNightRoom,
                            sp.GetRequiredService<IReservationCreator>(),
                            sp.GetRequiredService<IReservationProvider>(),
                            sp.GetRequiredService<IReservationRemover>(),
                            sp.GetRequiredService<IInitializationRooms>(),
                            sp.GetRequiredService<ISaveRoomsProvider>(),
                            sp.GetRequiredService<IEarningsWritting>(),
                            sp.GetRequiredService<IEarningReading>());
                    });

                    // Hotel is centralized an managed in a single location - HotelStore
                    services.AddSingleton(sp =>
                    {
                        return new HotelStore(sp.GetRequiredService<Hotel>());
                    });


                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton(sp => new MainWindow()
                    {
                       DataContext = sp.GetRequiredService<MainViewModel>()
                    }); 

                }).Build();

            // 
            //_navigationStore = new NavigationStore();
        }

        private ReservationsListingViewModel CreateReservationsListingViewModelInService(IServiceProvider sp)
        {
            return ReservationsListingViewModel.ReservationsListingViewModelBuilder(sp.GetRequiredService<HotelStore>(),
                                       sp.GetRequiredService<NavigationServiceWpf<MakeReservationViewModel>>(),
                                                              sp.GetRequiredService<NavigationServiceWpf<OverviewViewModel>>());
        }

        private EntranceToHotelViewModel CreateEntranceToHotelViewModelInService(IServiceProvider sp)
        {
           return EntranceToHotelViewModel.CreateEntranceToHotelViewModel(sp.GetRequiredService<HotelStore>(), 
                                     sp.GetRequiredService<NavigationServiceWpf<MakeReservationViewModel>>());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Start the host
            _host.Start();

            HotelManagementDbContextFactory hotelManagementDbContext = _host.Services.GetRequiredService<HotelManagementDbContextFactory>();
            // Migration will be created if it does not exist or is out of date
            using (HotelManagementDbContext dbContext = hotelManagementDbContext.CreateHotelManagementDbContext())
            {
                // We can use PMC commands to create the database and apply migrations
                // but I think for simple and small projects, we can also do it this way
                dbContext.Database.EnsureCreated();
                dbContext.Database.Migrate();
            }

            // Get the first view model and navigate to it
            NavigationServiceWpf<EntranceToHotelViewModel> navigationService = _host.Services.GetRequiredService<NavigationServiceWpf<EntranceToHotelViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();

            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
