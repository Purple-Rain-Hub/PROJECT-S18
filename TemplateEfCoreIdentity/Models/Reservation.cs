using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemplateEfCoreIdentity.Models
{
    
    public class Reservation
    {
        [Key]
        public Guid ReservationId { get; set; }
        public Guid ClientId { get; set; }
        public Guid RoomId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public bool HasEnded { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }


    }
}
