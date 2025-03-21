using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemplateEfCoreIdentity.Data;
using TemplateEfCoreIdentity.Models;
using TemplateEfCoreIdentity.ViewModels;

namespace TemplateEfCoreIdentity.Services
{
    public class ReservationService
    {
        private readonly PROJECT_S18DbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationService(PROJECT_S18DbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }
        }

        public async Task<ReservationsViewModel> GetAllReservationsAsync()
        {
            try
            {
                var reservationsList = new ReservationsViewModel();

                reservationsList.Reservations = await _context.Reservations.Include(r => r.User).Include(r => r.Client).Include(r => r.Room).ToListAsync();

                return reservationsList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Room>> GetRoomsAsync()
        {
            var rooms = await _context.Rooms
            .Where(r => r.Reservations.All(r => r.HasEnded == true)).OrderBy(r => r.Number).ToListAsync();

            return rooms;
        }
        public async Task<List<Room>> GetRoomsAsync(Guid roomId)
        {
            var rooms = await _context.Rooms.Where(r => r.Reservations.All(r => r.HasEnded == true) || r.RoomId == roomId).OrderBy(r=> r.Number).ToListAsync();

            return rooms;
        }

        public async Task<Client> CheckClient(string email)
        {
            var existingClient = await _context.Clients.Where(c => c.Email == email).FirstOrDefaultAsync();
            return existingClient;
        }

        public async Task<bool> AddReservationAsync(AddReservationViewModel model, bool newClient, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.FindByEmailAsync(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value);
            var clientId = new Guid();
            if (newClient)
            {
                var client = new Client()
                {
                    ClientId = Guid.NewGuid(),
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Phone = model.Phone
                };

                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                clientId = client.ClientId;
            }
            else
            {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == model.Email);
                client.Name = model.Name;
                client.Surname = model.Surname;
                client.Phone = model.Phone;
                client.Email = model.Email;

                await _context.SaveChangesAsync();
                clientId = client.ClientId;
            }

            var newReservation = new Reservation()
            {
                ReservationId = Guid.NewGuid(),
                ClientId = clientId,
                RoomId = model.RoomId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                UserId = user.Id
            };

            _context.Reservations.Add(newReservation);
            return await SaveAsync();
        }

        public async Task<Reservation> GetReservationById(Guid id)
        {
            var reservation = await _context.Reservations.Include(r => r.Client).Include(r => r.Room).FirstAsync(r => r.ReservationId == id);
            if (reservation == null)
            {
                return null;
            }

            return reservation;
        }

        public async Task<bool> UpdateReservation(EditReservationViewModel model)
        {
            var reservation = await _context.Reservations.Include(r=>r.Client).FirstOrDefaultAsync(r=> r.ReservationId == model.ReservationId);
            if (reservation == null)
            {
                return false;
            }

            reservation.ReservationId = model.ReservationId;
            reservation.StartDate = model.StartDate;
            reservation.EndDate = model.EndDate;
            reservation.RoomId = model.RoomId;
            reservation.Client.Email = model.Email;
            reservation.Client.Phone = model.Phone;


            return await SaveAsync();
        }
    }
}
