using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class EntranceToHotelViewModel : ViewModelBase
    {
        private readonly Hotel _hotel;
        public string HotelName => _hotel.Name;
        public ICommand EntranceToHotelCommand { get; }

        public EntranceToHotelViewModel(Hotel hotel, NavigationServiceWpf naviationToMakeReservation)
        {
            _hotel = hotel;

            EntranceToHotelCommand = new NavigateCommand(naviationToMakeReservation);
        }
    }
}
