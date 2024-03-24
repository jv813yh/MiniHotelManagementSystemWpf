using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HotelReservationsWpf.DbContexts
{
    public class HotelManagementDesignTimeDbContextFactory : IDesignTimeDbContextFactory<HotelManagementDbContext>
    {
        public HotelManagementDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=hotelManagement.db")
                .Options;

            return new HotelManagementDbContext(options);
        }
    }
}
