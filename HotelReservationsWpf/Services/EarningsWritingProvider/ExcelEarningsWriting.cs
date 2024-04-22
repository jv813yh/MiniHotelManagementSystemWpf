using ClosedXML.Excel;
using HotelReservationsWpf.DTOs;

namespace HotelReservationsWpf.Services.EarningsWritingProvider
{
    // Class for writing earnings to an Excel file 
    public class ExcelEarningsWriting : IEarningsWritting
    {

        /// <summary>
        /// Writes earnings to an Excel file 
        /// </summary>
        /// <param name="montlyEarnings"></param>
        public void WriteEarnings(List<MontlyEarningsDTO> montlyEarnings, string filePath)
        {
            // Represents a workbook in an Excel 
            using (var workBook = new XLWorkbook())
            {
                workBook.Style.Font.FontName = "Arial";
                workBook.Style.Font.FontSize = 12;


                // Add a worksheet
                var workSheet = workBook.Worksheets.Add("Earnings");

                workSheet.Cell(1,1).Value = $"Earnings report from {DateTime.Now.Year}";
                workSheet.Cell(1,1).Style.Font.Bold = true;
                workSheet.Cell(1,1).Style.Font.FontSize = 16;
                workSheet.Cell(1,1).Style.Font.FontColor = XLColor.Red;



                // Header
                workSheet.Cell(3, 1).Value = "Months";
                workSheet.Cell(3, 2).Value = "Earnings";
                workSheet.Cell(3, 1).Style.Font.Bold = true;
                workSheet.Cell(3, 1).Style.Font.FontColor = XLColor.Redwood;
                workSheet.Cell(3, 2).Style.Font.Bold = true;
                workSheet.Cell(3, 2).Style.Font.FontColor = XLColor.Redwood;

                // Data
                int row = 4;
                foreach(var earnings in montlyEarnings)
                {
                    workSheet.Cell(row, 1).Value = earnings.Month;
                    workSheet.Cell(row, 1).Style.DateFormat.Format = "MMMM";  
                    workSheet.Cell(row, 2).Value = earnings.Earnings;

                    row++;
                }

                // Save the Excel file
                workBook.SaveAs(filePath);
            }
        }
    }
}
