namespace HotelReservationsWpf.Models
{
    public class GuestPerson
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string PhoneNumber { get; }

        public GuestPerson(string firstName, string lastName, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"Guest: {FirstName} {LastName}.\nPhone number: {PhoneNumber} and email: {Email}";
        }
    }
}
