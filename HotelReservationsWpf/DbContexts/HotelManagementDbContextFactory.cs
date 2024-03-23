using Microsoft.EntityFrameworkCore;

namespace HotelReservationsWpf.DbContexts
{

    /* 
     *        The class is used to create a new instance of the HotelManagementDbContext
     *               class with factory method. The class has a constructor that takes a connection
     *                      string as a parameter. The CreateHotelManagementDbContext method creates a new
     *                             instance of the HotelManagementDbContext class with the connection string passed
     *                                    to the constructor. The CreateHotelManagementDbContext method returns the new
     *                                           instance of the HotelManagementDbContext class.
     *                                              */
    public class HotelManagementDbContextFactory
    {
        // Connection string to the database 
        private readonly string _connectionString;

        public HotelManagementDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        // 
        public HotelManagementDbContext CreateHotelManagementDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite(_connectionString)
                .Options;

            return new HotelManagementDbContext(options);
        }
    }
    
    /*

    public static class HotelManagementDbContextFactory
    {
        private const string _connectionString = "Data Source=hotelManagement.db";
        public static HotelManagementDbContext CreateHotelManagementDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite(_connectionString)
                .Options;

            return new HotelManagementDbContext(options);
        }
    }
    */
}
