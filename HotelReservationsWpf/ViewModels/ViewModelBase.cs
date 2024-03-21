using System.ComponentModel;

namespace HotelReservationsWpf.ViewModels
{
    // This class serves as a base class for all view models in the application.
    // It implements the INotifyPropertyChanged interface to provide property change notifications.
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // Method that triggers the PropertyChanged event
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
