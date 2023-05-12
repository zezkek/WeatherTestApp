using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Mvc;
using WeatherTestApp.Data;
using WeatherTestApp.Data.Enums;
using WeatherTestApp.Models;
using WeatherTestApp.ViewModels;

namespace WeatherTestApp.Controllers
{
    /// <summary>
    /// Контроллер страниц загрузки и просмотра данных о погоде
    /// </summary>
    public class WeatherPeriodController : Controller
    {
        private AppDbContext _appDbContext;
        private WeatherViewModel weatherViewModel;
        public WeatherPeriodController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            weatherViewModel = new WeatherViewModel
            {
                ItemsPerPage = 20,
                Periods = _appDbContext.WeatherPeriods.ToList().OrderBy(m => m.Date).ThenBy(m => m.Time),
                CurrentPage = 1,
                SelectedMonth = Month.None
            };
        }

        #region Methods

        /// <summary>
        /// Отфильтровывает таблицу по заданным параметрам
        /// </summary>
        /// <param name="page">Номер текущей страницы</param>
        /// <param name="curMonth">Выбранный месяц</param>
        /// <param name="selectedYear">Выбранный год</param>
        public IActionResult ViewWeather(int page = 1, Month curMonth = Month.None, int selectedYear = -1)
        {

            if (curMonth == Month.None)
                weatherViewModel.Periods = _appDbContext.WeatherPeriods.ToList().OrderBy(m => m.Date).ThenBy(m => m.Time);
            else
                weatherViewModel.Periods = _appDbContext.WeatherPeriods.ToList().Where(w => w.Date.Month == (int)curMonth).OrderBy(m => m.Date).ThenBy(m => m.Time);

            weatherViewModel.IncludedYears = IncludedYears();

            if (selectedYear != -1)
            {
                weatherViewModel.Periods = weatherViewModel.Periods.Where(y => y.Date.Year == selectedYear);
            }

            weatherViewModel.SelectedYear = selectedYear;
            weatherViewModel.CurrentPage = page;
            weatherViewModel.SelectedMonth = curMonth;

            weatherViewModel.CalculatePages();
            return View(weatherViewModel);
        }

        public IActionResult LoadWeather()
        {
            return View();
        }

        /// <summary>
        /// Проверяет на корректность загруженные таблицы, сохраняет данные в БД и переадресовывает на страницу просмотра данных
        /// </summary>
        /// <param name="files">Загруженные файлы</param>
        [HttpPost]
        public async Task<IActionResult> LoadFile(ICollection<IFormFile> files)
        {
            foreach (IFormFile file in files)
            {
                XLWorkbook xLWorkbook = new XLWorkbook(file.OpenReadStream());
                foreach (IXLWorksheet worksheet in xLWorkbook.Worksheets)
                {
                    IXLRangeRow rangeRow = worksheet.Row(3).RowUsed();
                    if (rangeRow.Cell(1).Value.ToString() == "Дата" && rangeRow.Cell(2).Value.ToString() == "Время" && rangeRow.Cell(3).Value.ToString() == "Т" && rangeRow.Cell(4).Value.ToString() == "Отн. влажность" &&
                        rangeRow.Cell(5).Value.ToString() == "Td" && rangeRow.Cell(6).Value.ToString() == "Атм. давление," && rangeRow.Cell(7).Value.ToString() == "Направление" && rangeRow.Cell(8).Value.ToString() == "Скорость" &&
                        rangeRow.Cell(9).Value.ToString() == "Облачность," && rangeRow.Cell(10).Value.ToString() == "h" && rangeRow.Cell(11).Value.ToString() == "VV" && rangeRow.Cell(12).Value.ToString() == "Погодные явления")
                    {
                        rangeRow = worksheet.Row(5).RowUsed();
                        for (int i = 5; i <= worksheet.Rows().Count(); i++)
                        {
                            WeatherPeriod weather = new WeatherPeriod()
                            {
                                Date = DateTime.Parse(rangeRow.Cell(1).Value.ToString()).Date,
                                Time = rangeRow.Cell(2).Value.ToString(),
                                Temperature = rangeRow.Cell(3).Value.ToString(),
                                Humidity = rangeRow.Cell(4).Value.ToString(),
                                Dew = rangeRow.Cell(5).Value.ToString(),
                                Pressure = rangeRow.Cell(6).Value.ToString(),
                                WindDirection = rangeRow.Cell(7).Value.ToString(),
                                WindSpeed = rangeRow.Cell(8).Value.ToString(),
                                Cloudiness = rangeRow.Cell(9).Value.ToString(),
                                CloudBase = rangeRow.Cell(10).Value.ToString(),
                                HorizontalVisibility = rangeRow.Cell(11).Value.ToString(),
                                WeatherConditions = rangeRow.Cell(12).Value.ToString()
                            };

                            await _appDbContext.WeatherPeriods.AddAsync(weather);

                            rangeRow = rangeRow.RowBelow();
                        }

                        await _appDbContext.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction("ViewWeather");
        }

        /// <summary>
        /// Высчитывает года, которые присутствуют в таблице погод
        /// </summary>
        /// <returns>Массив годов</returns>
        public List<int> IncludedYears()
        {
            List<int> years = new List<int>();
            foreach (WeatherPeriod weatherPeriod in _appDbContext.WeatherPeriods.ToList())
            {
                if (!years.Contains(weatherPeriod.Date.Year))
                    years.Add(weatherPeriod.Date.Year);
            }
            return years;
        }

        #endregion
    }
}
