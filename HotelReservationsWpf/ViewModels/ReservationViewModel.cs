using HotelReservationsWpf.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationsWpf.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private Reservation _reservation;
        public int RoomNumber => _reservation.CurrentRoom.RoomNumber;
        public RoomType RoomType => _reservation.CurrentRoom.RoomType;
        public string GuestName => _reservation.GuestName.FirstName + " " + _reservation.GuestName.LastName;
        public DateOnly CheckInDate => _reservation.CheckInDate;
        public DateOnly CheckOutDate => _reservation.CheckOutDate;
        public decimal TotalCost => _reservation.TotalCost;

        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;
        }
    }
}
