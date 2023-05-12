using DocumentFormat.OpenXml.Drawing.Charts;
using System.Reflection.Metadata;
using WeatherTestApp.Data.Enums;
using WeatherTestApp.Models;

namespace WeatherTestApp.ViewModels
{
    /// <summary>
    /// ViewModel для архива погод
    /// </summary>
    public class WeatherViewModel
    {
        #region Properties

        public IEnumerable<WeatherPeriod> Periods { get; set; }
        public IEnumerable<int> IncludedYears { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int SelectedYear { get; set; }
        public Month SelectedMonth { get; set; } = Month.None;

        #endregion

        #region Methods

        /// <summary>
        /// Вычисляет общее количество страниц с заданными параметрами
        /// </summary>
        /// <returns>Общее кол-во страниц</returns>
        public int TotalPages()
        {
            return (int)Math.Ceiling(Periods.Count() / (double)ItemsPerPage);
        }

        /// <summary>
        /// Вычисляет начальную и конечную позиции страниц навигации
        /// </summary>
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

        /// <summary>
        /// Вычисляет каике элементы должны выводиться пользователю
        /// </summary>
        /// <returns>Массив записей погод</returns>
        public IEnumerable<WeatherPeriod> PaginatedPeriods()
        {
            int start = (CurrentPage - 1) * ItemsPerPage;
            return Periods.OrderBy(b => b.Date).Skip(start).Take(ItemsPerPage);
        }

        #endregion
    }
}
