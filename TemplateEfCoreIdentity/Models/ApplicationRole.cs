using Microsoft.AspNetCore.Identity;

namespace TemplateEfCoreIdentity.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
