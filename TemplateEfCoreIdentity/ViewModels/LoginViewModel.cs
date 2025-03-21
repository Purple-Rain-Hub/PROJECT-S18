using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TemplateEfCoreIdentity.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public required string Email { get; set; }
        [Display(Name = "Password")]
        [PasswordPropertyText]
        [Required(ErrorMessage = "The password is required")]
        public required string Password { get; set; }
    }
}
