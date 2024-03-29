using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class ReservationsListingViewModel : ViewModelBase
    {
        private readonly HotelStore _hotelStore;

        private readonly ObservableCollection<ReservationViewModel> _reservations;

        public IEnumerable<ReservationViewModel>? GetAllReservations => _reservations;

        public bool IsReservationsEmpty => !_reservations.Any();
        public bool IsLoadingSpinner => !IsReservationsEmpty;

        public string ReservationsEmptyMessage => "No reservations have been made yet";
        public ICommand NavigateMakeReservationCommand { get; }
        public ICommand NaviateToOvervieCommand { get; }
        public ICommand LoadReservationsCommand { get; }

        public ReservationsListingViewModel(HotelStore hotelStore, NavigationServiceWpf navigationServiceToMakeReservation,
                    NavigationServiceWpf navigationServiceToOverview)
        {
            _hotelStore = hotelStore;

            _reservations = new ObservableCollection<ReservationViewModel>();
            
            // Commands 
            NavigateMakeReservationCommand = new NavigateCommand(navigationServiceToMakeReservation);
            NaviateToOvervieCommand = new NavigateCommand(navigationServiceToOverview);
            LoadReservationsCommand = new LoadReservationsCommand(_hotelStore, this);
        }

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
