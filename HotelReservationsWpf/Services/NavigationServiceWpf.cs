using HotelReservationsWpf.Stores;
using HotelReservationsWpf.ViewModels;

namespace HotelReservationsWpf.Services
{
    // This class is used to navigate to the next view model
    public class NavigationServiceWpf
    {
        // The current view model that is being displayed
        private readonly NavigationStore _navigationStore;

        // Function that returns the next view model
        private readonly Func<ViewModelBase> _executeViewModel;

        public NavigationServiceWpf(NavigationStore navigationStore, Func<ViewModelBase> executeViewModel)
        {
            _navigationStore = navigationStore;
            _executeViewModel = executeViewModel;
        }

        // Method that is called to navigate to the next view model
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _executeViewModel();
        }
    }
}
