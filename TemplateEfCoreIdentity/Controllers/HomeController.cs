using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TemplateEfCoreIdentity.Models;

namespace TemplateEfCoreIdentity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
