using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TemplateEfCoreIdentity.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public required string Email { get; set; }

        [PasswordPropertyText]
        [Required(ErrorMessage = "The password is required")]
        public required string Password { get; set; }
    }
}
