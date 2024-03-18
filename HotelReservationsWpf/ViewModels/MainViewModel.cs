using HotelReservationsWpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationsWpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; private set; }

        public MainViewModel(Hotel hotel)
        {
            // Set the current view model to the welcome view model
            CurrentViewModel = new EntranceToHotelViewModel(hotel);
        }
    }
}
