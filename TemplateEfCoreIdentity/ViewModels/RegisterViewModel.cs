using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TemplateEfCoreIdentity.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="First name is required")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage ="Last name is required")]
        public required string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Birth date is required")]
        public DateOnly BirthDate { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [PasswordPropertyText]
        public required string Password { get; set; }
        [Compare("Password", ErrorMessage ="Password doesn't match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public required string Role {  get; set; }
    }
}
