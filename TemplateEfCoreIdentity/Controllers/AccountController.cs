using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TemplateEfCoreIdentity.Services;
using TemplateEfCoreIdentity.ViewModels;

namespace TemplateEfCoreIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        private bool VerifyAuth()
        {
            return User.Identity.IsAuthenticated;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RegisterPage()
        {
            if (VerifyAuth())
            {
                TempData["notification"] = "Errore: sei già loggato";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Errore nella compilazione del form";
                return RedirectToAction("RegisterPage");
            }

            var result = await _accountService.AddUser(registerViewModel);

            if (!result)
            {
                TempData["error"] = "Errore nella add dell'User";
                return RedirectToAction("RegisterPage");
            }

            TempData["notification"] = "Registrazione effettuata con successo";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LoginPage()
        {
            if (VerifyAuth())
            {
                TempData["notification"] = "Errore: sei già loggato";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Errore nella compilazione del form";
                return RedirectToAction("LoginPage");
            }

            var result = await _accountService.LoginUser(loginViewModel);

            if (!result)
            {
                TempData["error"] = "Errore nella login dell'User";
                return RedirectToAction("LoginPage");
            }

            TempData["notification"] = "Login effettuato con successo";
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var result = await _accountService.Logout();

            if (!result)
            {
                TempData["notification"] = "Errore nel Logout dell'User";
                return RedirectToAction("Index", "Home");
            }

            TempData["notification"] = "Logout effettuato con successo";
            return RedirectToAction("Index", "Home");
        }
    }
}
