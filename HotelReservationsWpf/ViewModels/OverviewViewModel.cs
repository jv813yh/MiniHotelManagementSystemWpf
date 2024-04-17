﻿using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Stores;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class OverviewViewModel : ViewModelBase, IDisposable
    {
        // Fields
        private readonly HotelStore _hotelStore;

        // Room number and guest name properties for removing reservations
        private string _roomNumberString = string.Empty;
        public string RoomNumberString
        {
            get => _roomNumberString;
            set
            {
                _roomNumberString = value;
                OnPropertyChanged(nameof(RoomNumberString));
            }
        }

        private string _guestName = string.Empty;
        public string GuestName
        {
            get => _guestName;
            set
            {
                _guestName = value;
                OnPropertyChanged(nameof(GuestName));
            }
        }

        private decimal _totalIncome;
        public decimal TotalIncome
        {
            get => _totalIncome;

            set
            {
                _totalIncome = value;
                OnPropertyChanged(nameof(TotalIncome));
            }
        }

        //
        private SeriesCollection _roomSeries;
        public SeriesCollection RoomSeries
        {
            get => _roomSeries;

            set
            {
                _roomSeries = value;
                OnPropertyChanged(nameof(RoomSeries));
            }
        }

        public string HotelName
            => _hotelStore.HotelName;

        // Commands for navigating to the reservation creation view and the reservation listing view
        public ICommand NavigateToMakeReservationCommand { get; }
        public ICommand NavigateToReservationsListingCommand { get; }

        // Command for removing reservations
        public ICommand RemoveReservationCommand { get; }

        // Command for closing the application
        public ICommand CloseApplicationCommand { get; }


        // Constructor
        public OverviewViewModel(HotelStore hotelStore, NavigationServiceWpf navigateServiceMakeResv,
                    NavigationServiceWpf navigateServiceToResvListing)
        {
            _hotelStore = hotelStore;

            NavigateToMakeReservationCommand = new NavigateCommand(navigateServiceMakeResv);
            NavigateToReservationsListingCommand = new NavigateCommand(navigateServiceToResvListing);

            RemoveReservationCommand = new RemoveReservationCommand(_hotelStore, this);

            CloseApplicationCommand = new CloseApplicationCommand(_hotelStore);

            UpdateRoomStatus();

            _hotelStore.ReservationsChanged += OnReservationsChanged;

        }

        private void UpdateRoomStatus()
        {
            RoomSeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Free Standard Rooms",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_hotelStore.GetStatusStandardRoomsByHotelStore().Item1) } 
                },
                new ColumnSeries
                {
                    Title = "Occupied Standard Rooms",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_hotelStore.GetStatusStandardRoomsByHotelStore().Item2) } 
                },

                new ColumnSeries
                {
                    Title = "Free Deluxe Rooms",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_hotelStore.GetStatusDeluxeRoomsByHotelStore().Item1) },
                },
                new ColumnSeries
                {
                    Title = "Occupied Deluxe Rooms",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_hotelStore.GetStatusDeluxeRoomsByHotelStore().Item2) }
                },

                new ColumnSeries
                {
                    Title = "Free Suite Rooms",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_hotelStore.GetStatusSuiteRoomsByHotelStore().Item1) }
                },
                new ColumnSeries
                {
                    Title = "Occupied Suite Rooms",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_hotelStore.GetStatusSuiteRoomsByHotelStore().Item2) }
                },
            };
        }

        private void OnReservationsChanged()
        {
            TotalIncome = _hotelStore.TotalIncome;

            UpdateRoomStatus();
        }

        public void Dispose()
        {
            _hotelStore.ReservationsChanged -= OnReservationsChanged;
        }
    }
}
