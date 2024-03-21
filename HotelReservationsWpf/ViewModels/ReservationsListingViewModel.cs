using HotelReservationsWpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationsWpf.ViewModels
{
    public class ReservationsListingViewModel : ViewModelBase
    {
        private readonly Hotel _hotel;

        private readonly ObservableCollection<ReservationViewModel> _reservations;

        public IEnumerable<ReservationViewModel>? GetAllReservations => _reservations;

        public ReservationsListingViewModel(Hotel hotel)
        {
            _hotel = hotel;
            _reservations = new ObservableCollection<ReservationViewModel>();
        }

        public static ReservationsListingViewModel CreateReservationsListingViewModel(Hotel hotel)
        {
            ReservationsListingViewModel returnViewModel = new ReservationsListingViewModel(hotel);

            returnViewModel.LoadReservations();

            return returnViewModel;
        }

        private void LoadReservations()
        {
            Reservation reservation1 = new Reservation(new Room(1, RoomType.Standard, RoomStatus.Available, 100), 
                new GuestPerson("John", "Doe", "email", "phoneNumber"), new DateOnly(2022, 1, 1), new DateOnly(2022, 1, 2));

            Reservation reservation2 = new Reservation(new Room(1, RoomType.Suite, RoomStatus.Available, 500),
              new GuestPerson("Peter", "Kiki", "PeterKikiemail", "phoneNumber"), new DateOnly(2022, 1, 1), new DateOnly(2022, 1, 2));

            Reservation reservation3 = new Reservation(new Room(1, RoomType.Deluxe, RoomStatus.Available, 400),
              new GuestPerson("Abraham", "Dolutovsky", "email", "phoneNumber"), new DateOnly(2022, 1, 1), new DateOnly(2022, 1, 2));

            List<Reservation> temporaryList = new List<Reservation>()
            {
                reservation1,
                reservation2,
                reservation3
            };


            foreach(Reservation reservation in temporaryList)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }

        }
    }
}
