using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Services.ReservationProviders;
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

        public ICommand NavigateMakeReservationCommand { get; }
        public ICommand LoadReservationsCommand { get; }

        public ReservationsListingViewModel(HotelStore hotelStore, NavigationServiceWpf navigationServiceToMakeReservation)
        {
            _hotelStore = hotelStore;

            _reservations = new ObservableCollection<ReservationViewModel>();

            NavigateMakeReservationCommand = new NavigateCommand(navigationServiceToMakeReservation);
            LoadReservationsCommand = new LoadReservationsCommand(_hotelStore, this);
        }

        public static ReservationsListingViewModel CreateReservationsListingViewModel(HotelStore hotelStore, NavigationServiceWpf navigationServiceToMakeReservation)
        {
            ReservationsListingViewModel returnViewModel = new ReservationsListingViewModel(hotelStore, navigationServiceToMakeReservation);

            returnViewModel.LoadReservationsCommand.Execute(null);

            return returnViewModel;
        }

        public void LoadReservationsFromDb(IEnumerable<Reservation> reservationsFromDb)
        {
            _reservations.Clear();

            foreach (Reservation reservation in reservationsFromDb)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
    }
}
