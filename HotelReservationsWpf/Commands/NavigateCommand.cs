using HotelReservationsWpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationsWpf.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationServiceWpf _navigationService;

        public NavigateCommand(NavigationServiceWpf navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
