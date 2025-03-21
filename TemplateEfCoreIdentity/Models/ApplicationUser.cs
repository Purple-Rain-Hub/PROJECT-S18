using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TemplateEfCoreIdentity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required DateOnly BirthDate { get; set; }
        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
