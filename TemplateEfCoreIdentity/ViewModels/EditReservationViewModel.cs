using System.ComponentModel.DataAnnotations;
using TemplateEfCoreIdentity.Models;

namespace TemplateEfCoreIdentity.ViewModels
{
    public class EditReservationViewModel
    {
        public Guid ReservationId { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [MaxLength(50)]
        public required string Email { get; set; }
        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Phone number is not valid")]
        [MaxLength(10)]
        public required string Phone { get; set; }
        public List<Room>? Rooms { get; set; }
        [Required(ErrorMessage = "Room Id is required")]
        public required Guid RoomId { get; set; }
        [Display(Name = "Reservation start date")]
        [Required(ErrorMessage = "Reservation start date is required")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Reservation end date")]
        [Required(ErrorMessage = "Reservation end date is required")]
        public DateTime EndDate { get; set; }
    }
}
