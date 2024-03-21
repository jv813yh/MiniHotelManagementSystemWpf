using HotelReservationsWpf.ViewModels;

namespace HotelReservationsWpf.Stores
{
    // This class is used to store the current view model that is being displayed
    public class NavigationStore
    {
        // The current view model that is being displayed
        private ViewModelBase? _currentViewModel;
        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                // Notify that the current view model has changed
                OnCurrentViewModelChanged();
            }
        }

        // Event that is triggered when the current view model changes
        public event Action? CurrentViewModelChanged;

        // Method that triggers the CurrentViewModelChanged event
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
