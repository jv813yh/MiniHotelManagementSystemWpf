using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class ReservationsListingViewModel : ViewModelBase
    {
        private readonly Hotel _hotel;

        private readonly ObservableCollection<ReservationViewModel> _reservations;

        public IEnumerable<ReservationViewModel>? GetAllReservations => _reservations;

        public ICommand NavigateMakeReservationCommand { get; }

        public ReservationsListingViewModel(Hotel hotel, NavigationServiceWpf navigationServiceToMakeReservation)
        {
            _hotel = hotel;
            _reservations = new ObservableCollection<ReservationViewModel>();

            NavigateMakeReservationCommand = new NavigateCommand(navigationServiceToMakeReservation);
        }

        public static ReservationsListingViewModel CreateReservationsListingViewModel(Hotel hotel, NavigationServiceWpf navigationServiceToMakeReservation)
        {
            ReservationsListingViewModel returnViewModel = new ReservationsListingViewModel(hotel, navigationServiceToMakeReservation);

            returnViewModel.LoadReservations();

            return returnViewModel;
        }

        private void LoadReservations()
        {
            _reservations.Clear();

            foreach (Reservation reservation in _hotel.GetAllReservations())
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }

        }
    }
}
