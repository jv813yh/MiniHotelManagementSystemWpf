using HotelReservationsWpf.Stores;
using HotelReservationsWpf.ViewModels;

namespace HotelReservationsWpf.Services
{
    // This class is used to navigate to the next view model
    public class NavigationServiceWpf<TViewModel> where TViewModel : ViewModelBase
    {
        // The current view model that is being displayed
        private readonly NavigationStore _navigationStore;

        // Function that returns the next view model
        private readonly Func<TViewModel> _executeViewModel;

        public NavigationServiceWpf(NavigationStore navigationStore, Func<TViewModel> executeViewModel)
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
