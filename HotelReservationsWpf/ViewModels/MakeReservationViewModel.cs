﻿using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Models;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Stores;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        // Fields
        private readonly HotelStore _hotelStore;

        private string _firstName = string.Empty, _lastName = string.Empty, 
                       _phoneNumber = string.Empty, _emailAddress = string.Empty, 
                       _expectedPriceString = string.Empty;

        private DateTime _checkInDate = DateTime.Now, 
                         _checkOutDate = DateTime.Now;

        private RoomType? _roomType = null;


        // FirstName property with validation and error handling 
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));

                //

                RemoveErrors(nameof(FirstName));

                if(_firstName.Length < 3)
                {
                    HandleErrors(nameof(FirstName), "First name must be at least 3 characters long" );
                }

                if(IsNameNoValid(value))
                {
                    HandleErrors(nameof(FirstName), "First name must contain only letters");
                }

                if (FirstName.Contains(" "))
                {
                    HandleErrors(nameof(LastName), "First name can not contain empty space");
                }
            }
        }

        // LastName property with validation and error handling
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));

                //

                RemoveErrors(nameof(LastName));

                if (LastName.Length < 3)
                {
                    HandleErrors(nameof(LastName), "Last name must be at least 3 characters long");
                }

                if (IsNameNoValid(value))
                {
                    HandleErrors(nameof(LastName), "Last name must contain only letters");
                }

                if (LastName.Contains(" "))
                {
                    HandleErrors(nameof(LastName), "Last name can not contain empty space");
                }
            }
        }

        // CheckInDate property with validation and error handling
        public DateTime CheckInDate
        {
            get { return _checkInDate; }
            set
            {
                _checkInDate = value;

                // Inform the view that the CheckInDate 
                OnPropertyChanged(nameof(CheckInDate));


                //
                CalculateExpectedPrice(SelectedRoomType);

                //

                RemoveErrors(nameof(CheckInDate));

                if (CheckInDate.DayOfYear >= CheckOutDate.DayOfYear && CheckInDate.Year == CheckOutDate.Year)
                {
                    HandleErrors(nameof(CheckInDate), "The check in date cannot start before the check out date");
                }

                if (CheckInDate.DayOfYear < DateTime.Now.DayOfYear && CheckInDate.Year == CheckOutDate.Year)
                {
                    HandleErrors(nameof(CheckInDate), "The check in date cannot start before today");
                }

                if (CheckInDate.Year != DateTime.Now.Year)
                {
                    HandleErrors(nameof(CheckInDate), "The reservation must be made in one calendar year :(");
                }

                if (CheckOutDate.DayOfYear == CheckInDate.DayOfYear && CheckInDate.Year == CheckOutDate.Year)
                {
                    HandleErrors(nameof(CheckOutDate), "The check out date cannot be the same as the check in date");
                }

            }
        }

        // CheckOutDate property with validation and error handling
        public DateTime CheckOutDate
        {
            get { return _checkOutDate; }
            set
            {
                _checkOutDate = value;

                // Inform the view that the CheckInDate 
                OnPropertyChanged(nameof(CheckOutDate));

                // 
                CalculateExpectedPrice(SelectedRoomType);

                //

                RemoveErrors(nameof(CheckOutDate));

                if (CheckOutDate.DayOfYear < DateTime.Now.DayOfYear && CheckInDate.Year == CheckOutDate.Year)
                {
                    HandleErrors(nameof(CheckOutDate), "The check out date cannot start before today");
                }

                if (CheckOutDate.DayOfYear < CheckInDate.DayOfYear && CheckInDate.Year == CheckOutDate.Year)
                {
                    HandleErrors(nameof(CheckOutDate), "The check out date cannot start before the check in date");
                }

                if (CheckOutDate.DayOfYear == CheckInDate.DayOfYear && CheckInDate.Year == CheckOutDate.Year)
                {
                    HandleErrors(nameof(CheckOutDate), "The check out date cannot be the same as the check in date");
                }

                if (CheckOutDate.Year != DateTime.Now.Year)
                {
                    HandleErrors(nameof(CheckOutDate), "The reservation must be made in one calendar year :(");
                }

                if(CheckInDate < CheckOutDate && CheckInDate.Year == CheckOutDate.Year 
                    && CheckInDate.DayOfYear > DateTime.Now.DayOfYear 
                    && CheckOutDate.DayOfYear > DateTime.Now.DayOfYear)
                {
                    RemoveErrors(nameof(CheckInDate));
                }
            }
        }

        // PhomeNumber property with validation and error handling
        public string PhomeNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhomeNumber));

                // 

                RemoveErrors(nameof(PhomeNumber));

                if(!(_phoneNumber.Length == 10))
                {
                    HandleErrors(nameof(PhomeNumber), "Phone number must be 10 characters long");
                }

                if(!int.TryParse(value, out int temporaryPhoneNumber))
                {
                    HandleErrors(nameof(PhomeNumber), "Phone number must contain only digits");
                }

                if (_phoneNumber.Contains(" "))
                {
                    HandleErrors(nameof(PhomeNumber), "Phone number can not contain empty space");
                }
            }
        }
        
        // EmailAddress property with validation and error handling
        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;
                OnPropertyChanged(nameof(EmailAddress));

                // 

                RemoveErrors(nameof(EmailAddress));

                if (!EmailAddress.Contains('@'))
                {
                    HandleErrors(nameof(EmailAddress), "Email address must contain '@'");
                }

                if(EmailAddress.Length < 5)
                {
                    HandleErrors(nameof(EmailAddress), "Email address must be at least 5 characters long");
                }

                if(EmailAddress.Contains(" "))
                {
                    HandleErrors(nameof(EmailAddress), "Email address can not contain empty space");
                }
            }
        }

        // Property to get the selected room type for the reservation
        // It is used for rabio buttons in the view and calculate the expected price
        public RoomType? SelectedRoomType
        {
            get { return _roomType; }
            set
            {
                _roomType = value;
                OnPropertyChanged(nameof(SelectedRoomType));

                CalculateExpectedPrice(SelectedRoomType);
            }
        }

        // Property to get the expected price for the reservation
        // It is used to display the price in the view
        public string ExpectedPriceString
        {
            get { return _expectedPriceString; }

            set
            {
                if(CheckOutDate.DayOfYear != DateTime.Now.DayOfYear && SelectedRoomType != null
                    && !(CheckOutDate.DayOfYear < CheckInDate.DayOfYear) 
                        && !(CheckInDate.DayOfYear < DateTime.Now.DayOfYear))
                {
                    _expectedPriceString = value;
                    OnPropertyChanged(nameof(ExpectedPriceString));
                }
                else if(SelectedRoomType != null)
                {
                    _expectedPriceString = "Unfeasible";
                    OnPropertyChanged(nameof(ExpectedPriceString));
                }
            }
        }

        // Property to get the status of the standard room in the hotel store with GetStatusStandardRoomsHotelStore method
        // It is used to display the status standard room in the view
        public string GetStatusStandardRooms
        {
            get
            {
                (int available, int occupied) = _hotelStore.GetStatusStandardRoomsByHotelStore();
                return $"Occupied: {occupied} Available: {available}";
            }
        }

        // Property to get the status of the deluxe room in the hotel store with GetStatusDeluxeRoomsHotelStore method
        // It is used to display the status deluxe room in the view
        public string GetStatusDeluxeRooms
        {
            get
            {
                (int available, int occupied) = _hotelStore.GetStatusDeluxeRoomsByHotelStore();
                return $"Occupied: {occupied} Available: {available}";
            }
        }

        // Property to get the status of the suite room in the hotel store with GetStatusSuiteRoomsHotelStore method
        // It is used to display the status suite room  in the view
        public string GetStatusSuiteRooms
        {
            get
            {
                (int available, int occupied) = _hotelStore.GetStatusSuiteRoomsByHotelStore();
                return $"Occupied: {occupied} Available: {available}";
            }
        }

        //
        public ICommand RoomPreferenceCommand { get; }

        //
        public ICommand SubmitCommand { get; }

        //
        public ICommand NavigateToOverviewCommand { get; }


        /* 
         * 
         *    Implementation INotifyDataErrorInfo 
         * 
         */

        // Dictionary to store the error messages for each property
        private Dictionary<string, List<string>> _propertiesErrorsMessages;

        // Event handler for the ErrorsChanged event
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        // Method to raise the ErrorsChanged event
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        // Method to check if there are any errors
        public bool HasErrors 
            => _propertiesErrorsMessages.Any();

        // Method to get the errors for a specific property
        public IEnumerable GetErrors(string? propertyName)
            => _propertiesErrorsMessages.GetValueOrDefault(propertyName ?? string.Empty) ?? new List<string>();
       
        // Method to handle the errors
        private void HandleErrors(string propertyName, string errorMessage)
        {
            // Verify if the property already has List of errors
            if (_propertiesErrorsMessages.ContainsKey(propertyName))
            {
                _propertiesErrorsMessages[propertyName].Add(errorMessage);
            }
            else
            {
                _propertiesErrorsMessages.Add(propertyName, new List<string> { errorMessage });
            }

            // Invoke event that errors were changed
            OnErrorsChanged(propertyName);
        }

        private void RemoveErrors(string propertyName)
        {
            _propertiesErrorsMessages.Remove(propertyName);

            // Invoke event that errors were changed
            OnErrorsChanged(propertyName);
        }

        public MakeReservationViewModel(HotelStore hotelStore, NavigationServiceWpf<ReservationsListingViewModel> navigationServiceToReservationsListingViewModel,
                        NavigationServiceWpf<OverviewViewModel> navigationServiceToOverviewViewModel)
        {
            _hotelStore = hotelStore;

            SubmitCommand = new MakeReservationCommand(hotelStore, this, 
                new NavigateCommand<ReservationsListingViewModel>(navigationServiceToReservationsListingViewModel));
            
            RoomPreferenceCommand = new RelayCommand<string>(SetRoomType);

            // Initialize the dictionary for the error messages
            _propertiesErrorsMessages = new Dictionary<string, List<string>>();

            // Initialize the navigation command to the overview view
            NavigateToOverviewCommand = new NavigateCommand<OverviewViewModel>(navigationServiceToOverviewViewModel);
        }

        // Set the room type for the reservation 
        private void SetRoomType(string type)
        {
            switch (type)
            {
                case "Standard":
                    SelectedRoomType = RoomType.Standard;
                    break;
                case "Deluxe":
                    SelectedRoomType = RoomType.Deluxe;
                    break;
                case "Suite":
                    SelectedRoomType = RoomType.Suite;
                    break;
                default:
                    MessageBox.Show("Invalid room type", "Wrong room type",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }

        // Calculate the expected price for the reservation
        private void CalculateExpectedPrice(RoomType? roomType)
        {
            if (roomType == null)
            {
                ExpectedPriceString = "0.00 €";

            }

            switch (SelectedRoomType)
            {
                case RoomType.Standard:
                    ExpectedPriceString = (_hotelStore.GetPriceForStandardRoomByHotelStore() * 
                                                (CheckOutDate.DayOfYear - CheckInDate.DayOfYear)).ToString("0.00") + " €";
                    break;
                case RoomType.Deluxe:
                    ExpectedPriceString = (_hotelStore.GetPriceForDeluxeRoomByHotelStore() *
                                                (CheckOutDate.DayOfYear - CheckInDate.DayOfYear)).ToString("0.00") + " €";
                    break;
                case RoomType.Suite:
                    ExpectedPriceString = (_hotelStore.GetPriceForSuiteRoomByHotelStore() *
                                                (CheckOutDate.DayOfYear - CheckInDate.DayOfYear)).ToString("0.00") + " €";
                    break;
                default:
                    ExpectedPriceString = "0.00 €";
                    break;
            }
        }

        /// <summary>
        /// Check if the name contains only letters
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns> true if is not valid, false if is  valid</returns>
        public bool IsNameNoValid(string name)
        {

            foreach (char c in name)
            {
                if (!char.IsLetter(c))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
