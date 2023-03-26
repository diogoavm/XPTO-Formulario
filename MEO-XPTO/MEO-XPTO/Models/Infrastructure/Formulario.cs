using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace MEO_XPTO.Models.Infrastructure
{
    public class Formulario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public bool IsSubscribedToNewsletter { get; set; }

        public Formulario(string firstName, string lastName, string email, string phoneNumber, bool isSubscribedToNewsletter)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            IsSubscribedToNewsletter = isSubscribedToNewsletter;
        }
    }
}