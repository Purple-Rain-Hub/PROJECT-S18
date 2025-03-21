using System.ComponentModel.DataAnnotations;

namespace TemplateEfCoreIdentity.Models
{
    public class Client
    {
        [Key]
        public Guid ClientId { get; set; }
        [Required]
        [MaxLength(30)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public required string Surname { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public required string Email { get; set; }
        [Required]
        [Phone]
        [MaxLength(10)]
        public required string Phone { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
