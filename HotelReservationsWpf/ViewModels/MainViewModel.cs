using HotelReservationsWpf.Models;
using HotelReservationsWpf.Stores;

namespace HotelReservationsWpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(Hotel hotel, NavigationStore navigationStore)
        {
            // Set the current view model 
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnViewModelChanged;
        }

        private void OnViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
