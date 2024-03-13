using HotelReservationsWpf.Commands;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class EntranceToHotelViewModel
    {
        public ICommand EntranceToHotelCommand { get; }

        public EntranceToHotelViewModel()
        {
           // EntranceToHotelCommand = new EntranceToHotelCommand();
        }
    }
}
