using DocumentFormat.OpenXml.Drawing.Charts;
using System.Reflection.Metadata;
using WeatherTestApp.Data.Enums;
using WeatherTestApp.Models;

namespace WeatherTestApp.ViewModels
{
    public class WeatherViewModel
    {
        public IEnumerable<WeatherPeriod> Periods { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public Month selectedMonth { get; set; } = Month.None;
        public int TotalPages()
        {
            return (int)Math.Ceiling(Periods.Count() / (double)ItemsPerPage);
        }

        public void CalculatePages()
        {
            StartPage = CurrentPage - 5;
            EndPage = CurrentPage + 4;

            if (StartPage <= 0)
            {
                StartPage = 1;
                EndPage -= (StartPage - 1);
            }

            if (EndPage > TotalPages())
            {
                EndPage = TotalPages();
                if (EndPage > 10)
                {
                    StartPage = EndPage - 9;
                }
            }
        }

        public IEnumerable<WeatherPeriod> PaginatedPeriods()
        {
            int start = (CurrentPage - 1) * ItemsPerPage;
            return Periods.OrderBy(b => b.Date).Skip(start).Take(ItemsPerPage);
        }
    }
}
