using HotelReservationsWpf.DTOs;

namespace HotelReservationsWpf.Services.EarningsWritingProvider
{
    public interface IEarningsWritting
    {
        // Function to write earnings (MontlyEarningsDTO) to a file 
        void WriteEarnings(List<MontlyEarningsDTO> montlyEarnings, string filePath);
    }
}
