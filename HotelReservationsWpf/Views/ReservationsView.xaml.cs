using System.Windows;
using System.Windows.Controls;

namespace HotelReservationsWpf.Views
{
    /// <summary>
    /// Interaction logic for ReservationsView.xaml
    /// </summary>
    public partial class ReservationsView : UserControl
    {
        public ReservationsView()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs> ListViewLoaded;

        private void GuestsListView_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewLoaded?.Invoke(this, EventArgs.Empty);
        }

    }
}
