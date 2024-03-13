namespace HotelReservationsWpf.Models
{
    public class Hotel
    {
        private readonly ReservationsBook _reservationBooks;
        public string Name { get; }

        public Hotel(string name) 
        {
            Name = name;
            _reservationBooks = new ReservationsBook();
        }

        public void AddReservation(Reservation reservation)
        {
            _reservationBooks.MakeReservation(reservation);
        }

        public IEnumerable<Reservation> GetAllReservations()
         => _reservationBooks.GetAllReservations();
    }
}
