using HotelReservationsWpf.DTOs;

namespace HotelReservationsWpf.Services.EarningsReadingProvider
{
    public interface IEarningReading
    {
        // Function to read earnings from a file
        List<MontlyEarningsDTO> ReadEarnings(string filePath);
    }
}
