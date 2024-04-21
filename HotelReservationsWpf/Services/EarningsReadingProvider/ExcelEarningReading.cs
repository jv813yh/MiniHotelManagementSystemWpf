using ClosedXML.Excel;
using HotelReservationsWpf.DTOs;
using System.Windows;

namespace HotelReservationsWpf.Services.EarningsReadingProvider
{
    // Class to read earnings from an Excel file 
    public class ExcelEarningReading : IEarningReading
    {
        private readonly string _filePath;

        public ExcelEarningReading(string filePath)
        {
            _filePath = filePath;
        }
        public List<MontlyEarningsDTO> ReadEarnings()
        {

            List<MontlyEarningsDTO> monthlyEarnings = new List<MontlyEarningsDTO>();

            try
            {
                using (XLWorkbook workbook = new XLWorkbook(_filePath))
                {
                    // First worksheet in the Excel file
                    IXLWorksheet worksheet = workbook.Worksheet(1);

                    int rowCount = worksheet.RowsUsed().Count();

                    for (int i = 4; i < rowCount + 2; i++) 
                    {
                        // Value in the first column
                        string month = worksheet.Cell(i, 1).Value.ToString();

                        // Value in the second column
                        //string earningString = worksheet.Cell(i, 2).Value.ToString();
                        Decimal.TryParse(worksheet.Cell(i, 2).Value.ToString(), out decimal earnings);

                        // Add the data to the list
                        monthlyEarnings.Add(new MontlyEarningsDTO { Month = month, Earnings = earnings });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading data from Excel: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return monthlyEarnings;
        }
    }
}
