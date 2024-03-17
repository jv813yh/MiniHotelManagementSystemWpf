﻿using HotelReservationsWpf.Exceptions;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.ViewModels;
using System.ComponentModel;
using System.Windows;

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

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        // Responding to changes in the UI to verify whether a reservation can be made
        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_viewModel.FirstName) || e.PropertyName == nameof(_viewModel.LastName) 
                    || e.PropertyName == nameof(_viewModel.PhomeNumber) || e.PropertyName == nameof(_viewModel.EmailAddress) 
                        || e.PropertyName == nameof(_viewModel.CheckInDate) || e.PropertyName == nameof(_viewModel.CheckOutDate)
                            || e.PropertyName == nameof(_viewModel.RoomTypeProperty))
            {
                OnCanExecuteChanged();
            }
        }
        // Verification of input values for the correct creation of a reservation
        public override bool CanExecute(object? parameter)
        {
            bool isAvailablePreferenceRoom = false;

            // Check if the first name, last name, phone number, and email address are not empty
            if (string.IsNullOrEmpty(_viewModel.FirstName) || string.IsNullOrEmpty(_viewModel.LastName) 
                    || string.IsNullOrEmpty(_viewModel.PhomeNumber) || string.IsNullOrEmpty(_viewModel.EmailAddress))
            {
                return false;
            }

            // Check if the check-in date is not greater than the check-out date
            // Check if the check-in date and check-out date are not in the past
            if(_viewModel.CheckInDate.DayOfYear >= _viewModel.CheckOutDate.DayOfYear
                || _viewModel.CheckInDate.DayOfYear < DateTime.Now.DayOfYear
                    || _viewModel.CheckOutDate.DayOfYear < DateTime.Now.DayOfYear)
            {
                return false;
            }

            // Check if the selected room type is available
            if (_viewModel.Hotel.IsAvailablePreferenceRoom(_viewModel.RoomTypeProperty))
            {
                isAvailablePreferenceRoom = true;
            }
            

            return base.CanExecute(parameter) && isAvailablePreferenceRoom;
        }

        // After verification of input data, reservation will be make
        public override void Execute(object? parameter)
        {
            try
            {
                /*
                Reservation newReservation = new Reservation(new Room(
               _viewModel.RoomTypeProperty, _viewModel.FirstName, _viewModel.LastName,
                                                     _viewModel.PhomeNumber, _viewModel.EmailAddress, _viewModel.CheckInDate, _viewModel.CheckOutDate);
                */
               // _hotel.CreateReservation(newReservation);
            }
            catch(ConflictReservationsException)
            {
                MessageBox.Show("The selected room type is not available for the selected dates.\n" +
                    "Please select another room type or change the dates.", "Conflict reservations error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while making the reservation", ex);
            }
        }
    }
}
