using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TemplateEfCoreIdentity.Models;
using Microsoft.EntityFrameworkCore;

namespace TemplateEfCoreIdentity.Data
{
    public class PROJECT_S18DbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public PROJECT_S18DbContext(DbContextOptions<PROJECT_S18DbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.User).WithMany(u => u.ApplicationUserRoles).HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.Role).WithMany(r => r.ApplicationUserRoles).HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<ApplicationUserRole>().Property(p => p.Date).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<Client>().HasIndex(e => e.Email).IsUnique();

            modelBuilder.Entity<Room>().HasIndex(r => r.Number).IsUnique();

            modelBuilder.Entity<Reservation>().Property(r => r.ReservationDate).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<Reservation>().HasOne(r => r.Client).WithMany(c => c.Reservations).HasForeignKey(r => r.ClientId);
            
            modelBuilder.Entity<Reservation>().HasOne(r => r.Room).WithMany(c => c.Reservations).HasForeignKey(r => r.RoomId);
            
            modelBuilder.Entity<Reservation>().HasOne(r => r.User).WithMany(u => u.Reservations).HasForeignKey(r => r.UserId);
        }
    }
}
