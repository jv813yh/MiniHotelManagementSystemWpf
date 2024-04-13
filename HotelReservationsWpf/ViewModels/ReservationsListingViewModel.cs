using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Views;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Data;

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

        // Check if the reservations collection is empty or not
        // Is uses for loading spinner visibility
        public bool IsLoadingSpinner 
            => !_reservations.Any();

        // If the reservations collection is empty, show the message
        public string ReservationsEmptyMessage 
            => "No reservations have been made yet";

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
