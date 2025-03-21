using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateEfCoreIdentity.Services;

namespace TemplateEfCoreIdentity.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> ListReservations()
        {
            try
            {
                var reservationsList = await _reservationService.GetAllReservationsAsync();
                if (reservationsList == null)
                {
                    throw new Exception();
                }
                return PartialView("_ReservationList", reservationsList);
            }
            catch
            {
                TempData["error"] = "Error in the loading of the reservations";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
