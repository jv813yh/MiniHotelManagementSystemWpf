using HotelReservationsWpf.Services;

namespace HotelReservationsWpf.Commands
{
    public class NavigateCommand : CommandBase
    {
        // NavigationServiceWpf calls Navigate() function to navigation to the next view model
        private readonly NavigationServiceWpf _navigationService;

        public NavigateCommand(NavigationServiceWpf navigationService)
        {
            _navigationService = navigationService;
        }

        // Function, actually implements Execute, calls Navigate() function from NavigationServiceWpf
        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
