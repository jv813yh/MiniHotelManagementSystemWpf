﻿using HotelReservationsWpf.Commands;
using HotelReservationsWpf.Services;
using HotelReservationsWpf.Stores;
using System.Windows.Input;

namespace HotelReservationsWpf.ViewModels
{
    public class EntranceToHotelViewModel : ViewModelBase
    {
        private readonly HotelStore _hotelStore;
        public string HotelName => _hotelStore.HotelName;
        public ICommand EntranceToHotelCommand { get; }

        public EntranceToHotelViewModel(HotelStore hotelStore, NavigationServiceWpf naviationToMakeReservation)
        {
            _hotelStore = hotelStore;

            EntranceToHotelCommand = new NavigateCommand(naviationToMakeReservation);
        }
    }
}
