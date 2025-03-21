using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateEfCoreIdentity.Models;
using TemplateEfCoreIdentity.Services;
using TemplateEfCoreIdentity.ViewModels;

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

        public async Task<IActionResult> AddReservationPage(AddReservationViewModel addReservationViewModel)
        {
            var rooms = await _reservationService.GetRoomsAsync();
            addReservationViewModel.Rooms = rooms;
            return View(addReservationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CheckEmail(string email, AddReservationViewModel addReservationViewModel)
        {
            try
            {
                var rooms = await _reservationService.GetRoomsAsync();
                addReservationViewModel.Rooms = rooms;
                var existingClient = await _reservationService.CheckClient(email);
                if (existingClient == null)
                {
                    return PartialView("_AddReservation", addReservationViewModel);
                }

                var client = new Client()
                {
                    Email = existingClient.Email,
                    Name = existingClient.Name,
                    Surname = existingClient.Surname,
                    Phone = existingClient.Phone,
                };
                ViewBag.Client = client;

                return PartialView("_AddReservation", addReservationViewModel);
            }
            catch
            {
                TempData["error"] = "Error in the check of the client";
                return RedirectToAction("AddReservationPage");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation(AddReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Errore nella compilazione del form";
                return RedirectToAction("AddReservationPage");
            }

            bool result = false;
            var existingClient = await _reservationService.CheckClient(model.Email);
            if (existingClient == null)
            {
                result = await _reservationService.AddReservationAsync(model, true, User);
            }
            else
            {
                result = await _reservationService.AddReservationAsync(model, false, User);
            }

            if (!result)
            {
                TempData["error"] = "Error in the reservation add";
                return RedirectToAction("Index", "Home");
            }

            TempData["notification"] = "Reservation Successiful";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(Guid id, EditReservationViewModel editReservationViewModel)
        {
            
            var reservation = await _reservationService.GetReservationById(id);

            var rooms = await _reservationService.GetRoomsAsync(reservation.RoomId);
            editReservationViewModel.Rooms = rooms;

            EditReservationViewModel reservationEdit = new()
            {
                ReservationId = id,
                Email = reservation.Client.Email,
                Phone = reservation.Client.Phone,
                RoomId = reservation.RoomId,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Rooms = rooms
            };

            return PartialView("_ReservationEdit", reservationEdit);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit(EditReservationViewModel editReservationViewModel)
        {
            ModelState.Remove("Rooms");
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    message = "Errore nel modello del form",
                    success = false
                });

            }

            var result = await _reservationService.UpdateReservation(editReservationViewModel);

            return Json(new
            {
                success = result,
                message = result ? "Update success" : "Error in the entity update on Db"
            });
        }
    }
}
