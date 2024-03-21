using HotelReservationsWpf.Stores;
using HotelReservationsWpf.ViewModels;

namespace HotelReservationsWpf.Services
{
    public class NavigationServiceWpf
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _executeViewModel;

        public NavigationServiceWpf(NavigationStore navigationStore, Func<ViewModelBase> executeViewModel)
        {
            _navigationStore = navigationStore;
            _executeViewModel = executeViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _executeViewModel();
        }
    }
}
