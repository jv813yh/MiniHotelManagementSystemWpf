using HotelReservationsWpf.Models;
using HotelReservationsWpf.ViewModels;
using System.ComponentModel;

namespace HotelReservationsWpf.Commands
{
    public class MakeReservationCommand : CommandBase
    {
        private readonly Hotel _hotel;
        private readonly MakeReservationViewModel _viewModel;
        private readonly NavigateCommand _navigateCommand;

        public MakeReservationCommand(Hotel hotel, MakeReservationViewModel makeReservationViewModel, NavigateCommand navigateCommand)
        {
            _hotel = hotel;
            _viewModel = makeReservationViewModel;
            _navigateCommand = navigateCommand;

            _viewModel.PropertyChanged += OnPropertyChanged;
        }

        // Responding to changes in the UI to verify whether a reservation can be made
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_viewModel.FirstName) || e.PropertyName == nameof(_viewModel.LastName) 
                    || e.PropertyName == nameof(_viewModel.PhomeNumber) || e.PropertyName == nameof(_viewModel.EmailAddress) 
                        || e.PropertyName == nameof(_viewModel.CheckInDate) || e.PropertyName == nameof(_viewModel.CheckOutDate))
            {
                OnCanExecuteChanged();
            }
        }
        // Verification of input values for the correct creation of a reservation
        public override bool CanExecute(object? parameter)
        {
            // Check if the check-in date is not greater than the check-out date
            // Check if the check-in date and check-out date are not in the past
            if(_viewModel.CheckInDate >= _viewModel.CheckOutDate ||
                _viewModel.CheckInDate < DateTime.Now || _viewModel.CheckOutDate < DateTime.Now)
            {
                return false;
            }

            // Check if the first name, last name, phone number, and email address are not empty
            if (string.IsNullOrEmpty(_viewModel.FirstName) || string.IsNullOrEmpty(_viewModel.LastName) 
                || string.IsNullOrEmpty(_viewModel.PhomeNumber) || string.IsNullOrEmpty(_viewModel.EmailAddress))
            {
                return false;
            }

            return base.CanExecute(parameter);
        }

        // After verification of input data, reservation will be make
        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
