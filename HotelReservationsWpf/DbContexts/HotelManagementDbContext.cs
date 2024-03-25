using HotelReservationsWpf.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationsWpf.DbContexts
{
    public class HotelManagementDbContext : DbContext
    {
        public DbSet<ReservationDTO> Reservations { get; set; }

        public HotelManagementDbContext(DbContextOptions options) : base(options)
        {
        }


    }
}
