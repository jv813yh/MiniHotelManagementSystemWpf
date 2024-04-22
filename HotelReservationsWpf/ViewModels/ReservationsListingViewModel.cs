using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class ReservationsListingViewModel : ViewModelBase
    {
        private readonly HotelStore _hotelStore;

        // Main collection of reservations
        private readonly ObservableCollection<ReservationViewModel> _reservations;

        // ListCollectionView implements the ICollectionView interface
        // and provides basic functionality for filtering, sorting,
        // and grouping items in a collection.
        public ICollectionView GuestsCollectionListView { get; }

        // Property for the loading spinner
        private bool _isLoadingSpinner = true;
        public bool IsLoadingSpinner 
        { 
            get => _isLoadingSpinner;
            set
            {
                if (_isLoadingSpinner != value)
                {
                    _isLoadingSpinner = value;
                    OnPropertyChanged(nameof(IsLoadingSpinner));
                }
            } 
        }


        // If the reservations collection is empty, show the message
        public string ReservationsEmptyMessage 
            => "No reservations have been made yet";

        private bool _isReservationsEmpty = false;
        public bool IsReservationsEmpty
        {
            get => _isReservationsEmpty;
            set
            {
                if (_isReservationsEmpty != value)
                {
                    _isReservationsEmpty = value;
                    OnPropertyChanged(nameof(IsReservationsEmpty));
                }
            }
        }

        // If we have reservations, show the list view
        public bool _isNeededListView = false;
        public bool IsNeededListView
        {
            get => _isNeededListView;
            set
            {
                if (_isNeededListView != value)
                {
                    _isNeededListView = value;
                    OnPropertyChanged(nameof(IsNeededListView));
                }
            }
        }

        // Commands
        public ICommand NavigateMakeReservationCommand { get; }
        public ICommand NaviateToOvervieCommand { get; }
        public ICommand LoadReservationsCommand { get; }
        public ICommand OrderByCommand { get; }

        public ReservationsListingViewModel(HotelStore hotelStore, NavigationServiceWpf navigationServiceToMakeReservation,
                    NavigationServiceWpf navigationServiceToOverview)
        {
            _hotelStore = hotelStore;

            _reservations = new ObservableCollection<ReservationViewModel>();
            
            // Commands 
            NavigateMakeReservationCommand = new NavigateCommand(navigationServiceToMakeReservation);
            NaviateToOvervieCommand = new NavigateCommand(navigationServiceToOverview);
            LoadReservationsCommand = new LoadReservationsCommand(_hotelStore, this);

            // Get the default view of the reservations collection
            GuestsCollectionListView = CollectionViewSource.GetDefaultView(_reservations);

            OrderByCommand = new OrderByCommand(GuestsCollectionListView);
        }

        // Builder method for the ReservationsListingViewModel
        public static ReservationsListingViewModel ReservationsListingViewModelBuilder(HotelStore hotelStore, 
                        NavigationServiceWpf navigationServiceToMakeReservation,
                        NavigationServiceWpf navigationServiceToOverview)
        {
            ReservationsListingViewModel returnViewModel = new ReservationsListingViewModel(hotelStore, 
                                                                    navigationServiceToMakeReservation, navigationServiceToOverview);


            // Lazy loading reservations from the database
            returnViewModel.LoadReservationsCommand.Execute(null);

            return returnViewModel;
        }

        // Load reservations from the database to the view model 
        public void LoadReservationsFromDb(IEnumerable<Reservation> reservationsFromDb)
        {
            _reservations.Clear();

            foreach (Reservation currentReservation in reservationsFromDb)
            {
                _reservations.Add(new ReservationViewModel(currentReservation));
            }
        }
    }
}
