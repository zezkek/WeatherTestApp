using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using WeatherTestApp.Data;
using WeatherTestApp.Models;

namespace WeatherTestApp.Controllers
{
    public class WeatherPeriodController : Controller
    {
        private AppDbContext _appDbContext;
        public WeatherPeriodController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult ViewWeather()
        {
            List<WeatherPeriod> weathers = _appDbContext.WeatherPeriods.ToList();
            return View(weathers);
        }

        public IActionResult LoadWeather()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadFile(ICollection<IFormFile> files)
        {
            if (files.Count == 0)
            {

            }

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
                                Date = rangeRow.Cell(1).Value.ToString(),
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
    }
}
