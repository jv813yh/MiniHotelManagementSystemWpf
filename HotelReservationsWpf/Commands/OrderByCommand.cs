using HotelReservationsWpf.ViewModels;
using System.ComponentModel;

namespace HotelReservationsWpf.Commands
{
    public class OrderByCommand : CommandBase
    {
        //CollectionView provides a window over a bound list that allows
        //you to customize the way items are sorted, grouped, and filtered ...
        private readonly ICollectionView _collectionView;

        public OrderByCommand(ICollectionView collectionView)
        {
            _collectionView = collectionView;
        }

        // Check if the command can be executed
        public override bool CanExecute(object? parameter)
         => (_collectionView == null) ? false : true;

        // Order reservations by different properties
        public override void Execute(object? parameter)
        {
            if (parameter == null)
            {
                return;
            }

            _collectionView.SortDescriptions.Clear();

            switch (parameter)
            {
                case "StartDateAscending":

                    // Order by CheckInDate in ascending order
                    _collectionView.SortDescriptions
                        .Add(new SortDescription("CheckInDate", ListSortDirection.Ascending));

                    break;

                case "StartDateDescending":

                    // Order by CheckInDate in descending order
                    _collectionView.SortDescriptions
                        .Add(new SortDescription("CheckInDate", ListSortDirection.Descending));

                    break;

                case "EndDateAscending":

                    // Order by CheckOutDate in ascending order
                    _collectionView.SortDescriptions
                        .Add(new SortDescription("CheckOutDate", ListSortDirection.Ascending));

                    break;

                case "EndDateDescending":

                    // Order by CheckOutDate in descending order
                    _collectionView.SortDescriptions
                        .Add(new SortDescription("CheckOutDate", ListSortDirection.Descending));

                    break;

                case "PriceAscending":

                    // Order by TotalCost in ascending order
                    _collectionView.SortDescriptions
                        .Add(new SortDescription("TotalCost", ListSortDirection.Ascending));

                    break;

                case "PriceDescending":

                    // Order by TotalCost in descending order
                    _collectionView.SortDescriptions
                        .Add(new SortDescription("TotalCost", ListSortDirection.Descending));

                    break;

                default:
                    break;
            }
        }
    }
}
