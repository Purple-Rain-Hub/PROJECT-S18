using Microsoft.EntityFrameworkCore;
using TemplateEfCoreIdentity.Data;
using TemplateEfCoreIdentity.Models;
using TemplateEfCoreIdentity.ViewModels;

namespace TemplateEfCoreIdentity.Services
{
    public class ReservationService
    {
        private readonly PROJECT_S18DbContext _context;

        public ReservationService(PROJECT_S18DbContext context)
        {
            _context = context;
        }

        public async Task<ReservationsViewModel> GetAllReservationsAsync()
        {
            try
            {
                var reservationsList = new ReservationsViewModel();

                reservationsList.Reservations = await _context.Reservations.Include(r=> r.User).Include(r => r.Client).Include(r => r.Room).ToListAsync();

                return reservationsList;
            }
            catch
            {
                return null;
            }
        }
    }
}
