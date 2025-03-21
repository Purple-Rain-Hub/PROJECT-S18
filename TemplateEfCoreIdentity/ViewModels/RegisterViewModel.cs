using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TemplateEfCoreIdentity.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage ="First name is required")]
        public required string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage ="Last name is required")]
        public required string LastName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public required string Email { get; set; }
        [Display(Name = "Birth date")]
        [Required(ErrorMessage = "Birth date is required")]
        public DateOnly BirthDate { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [PasswordPropertyText]
        public required string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password doesn't match")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role is required")]
        public required string Role {  get; set; }
    }
}
