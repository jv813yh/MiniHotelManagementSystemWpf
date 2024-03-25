namespace HotelReservationsWpf.Models
{
    // This class is used to represent a guest person in the hotel
    public class GuestPerson
    {
        public string GuestName { get; } 

        public string Email { get; }

        public string PhoneNumber { get; }

        public GuestPerson(string guestName, string email, string phoneNumber)
        {
            GuestName = guestName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"{GuestName}";
        }

        /*
        public override string ToString()
        {
            return $"Guest: {FirstName} {LastName}.\nPhone number: {PhoneNumber} and email: {Email}";
        }
        */
    }
}
