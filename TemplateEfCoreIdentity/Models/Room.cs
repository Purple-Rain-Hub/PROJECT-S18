using System.ComponentModel.DataAnnotations;

namespace TemplateEfCoreIdentity.Models
{
    public class Room
    {
        [Key]
        public Guid RoomId { get; set; }
        [Required]
        public required int Number {  get; set; }
        [Required]
        public required string Type { get; set; }
        [Required]
        public required double Price { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
