using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationsWpf.Models
{
    public class GuestPerson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public Guid GuestId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public GuestPerson(string firstName, string lastName, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        /*
        public override string ToString()
        {
            return $"Guest: {FirstName} {LastName}.\nPhone number: {PhoneNumber} and email: {Email}";
        }
        */
    }
}
