using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TemplateEfCoreIdentity.Models;
using Microsoft.AspNetCore.Authentication;
using TemplateEfCoreIdentity.ViewModels;
using Microsoft.EntityFrameworkCore;
using TemplateEfCoreIdentity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TemplateEfCoreIdentity.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _contextAccessor = contextAccessor; 
        }

        public async Task<bool> AddUser(RegisterViewModel model)
        {
            var newUser = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                BirthDate = model.BirthDate
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                return false;
            }

            var user = await _userManager.FindByEmailAsync(newUser.Email);

            if (user == null)
            {
                return false;
            }

            await _userManager.AddToRoleAsync(user, model.Role);

            return true;
        }

        public async Task<bool> LoginUser (LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return false;
            }

            var signinResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (!signinResult.Succeeded)
            {
                return false;
            }

            var claims = new List<Claim>();

            var nameClaim = new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}");

            var emailClaim = new Claim(ClaimTypes.Email, user.Email);

            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }

            claims.Add(nameClaim);
            claims.Add(emailClaim);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return true;
        }

        [Authorize]
        public async Task<bool> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}
