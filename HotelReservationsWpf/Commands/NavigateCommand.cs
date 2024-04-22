using HotelReservationsWpf.Services;
using HotelReservationsWpf.ViewModels;

namespace HotelReservationsWpf.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        // NavigationServiceWpf calls Navigate() function to navigation to the next view model
        private readonly NavigationServiceWpf<TViewModel> _navigationService;

        public NavigateCommand(NavigationServiceWpf<TViewModel> navigationService)
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
