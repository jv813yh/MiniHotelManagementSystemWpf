using HotelReservationsWpf.Models;
using HotelReservationsWpf.Stores;

namespace HotelReservationsWpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // The current view model that is being displayed
        private readonly NavigationStore _navigationStore;

        // Property that returns the current view model (binding to this property in MainWindow.xaml)
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(Hotel hotel, NavigationStore navigationStore)
        {
            // Set the current view model 
            _navigationStore = navigationStore;

            // Subscribe to the event that is triggered when the current view model changes
            _navigationStore.CurrentViewModelChanged += OnViewModelChanged;
        }

        // Method that is called when the current view model changes
        private void OnViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
