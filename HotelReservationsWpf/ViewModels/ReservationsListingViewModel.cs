using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Services.ReservationProviders;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class ReservationsListingViewModel : ViewModelBase
    {
        private readonly Hotel _hotel;

        private readonly ObservableCollection<ReservationViewModel> _reservations;

        private readonly IReservationProvider _reservationProvider;

        public IEnumerable<ReservationViewModel>? GetAllReservations => _reservations;

        public ICommand NavigateMakeReservationCommand { get; }
        public ICommand LoadReservationsCommand { get; }

        public ReservationsListingViewModel(Hotel hotel, NavigationServiceWpf navigationServiceToMakeReservation,
                            IReservationProvider reservationProvider)
        {
            _hotel = hotel;
            _reservationProvider = reservationProvider;

            _reservations = new ObservableCollection<ReservationViewModel>();

            NavigateMakeReservationCommand = new NavigateCommand(navigationServiceToMakeReservation);
            LoadReservationsCommand = new LoadReservationsCommand(_reservationProvider, this);
        }

        public static ReservationsListingViewModel CreateReservationsListingViewModel(Hotel hotel, NavigationServiceWpf navigationServiceToMakeReservation,
                                                        IReservationProvider reservationProvider)
        {
            ReservationsListingViewModel returnViewModel = new ReservationsListingViewModel(hotel, navigationServiceToMakeReservation, reservationProvider);

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
