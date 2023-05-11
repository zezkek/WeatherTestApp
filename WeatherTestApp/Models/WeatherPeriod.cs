using System.ComponentModel;

namespace WeatherTestApp.Models
{
    public class WeatherPeriod : BaseObject
    {

        #region Constuctror

        public WeatherPeriod() { }

        #endregion

        #region Fields

        private string? _date;
        private string? _time;
        private string? _temperature;
        private string? _humidity;
        private string? _dew;
        private string? _pressure;
        private string? _windDirection;
        private string? _windSpeed;
        private string? _cloudiness;
        private string? _cloudBase;
        private string? _horizontalVisibility;
        private string? _weatherConditions;

        #endregion

        #region Properties
        [DisplayName("Дата")]
        public string? Date
        {
            get => _date;
            set => _date = value;
        }

        [DisplayName("Время")]
        public string? Time
        {
            get => _time;
            set => _time = value;
        }

        [DisplayName("Температура")]
        public string? Temperature
        {
            get => _temperature;
            set => _temperature = value;
        }

        [DisplayName("Влажность воздуха %")]
        public string? Humidity
        {
            get => _humidity; 
            set => _humidity = value;
        }

        [DisplayName("Точка росы")]
        public string? Dew
        {
            get => _dew;
            set => _dew = value;
        }

        [DisplayName("Давление мм рт. ст.")]
        public string? Pressure
        {
            get => _pressure; 
            set => _pressure = value;
        }

        [DisplayName("Направление ветра")]
        public string? WindDirection
        {
            get => _windDirection; 
            set => _windDirection = value;
        }

        [DisplayName("Скорость ветра")]
        public string? WindSpeed
        {
            get => _windSpeed; 
            set => _windSpeed = value;
        }

        [DisplayName("Облачность %")]
        public string? Cloudiness
        {
            get => _cloudiness; 
            set => _cloudiness = value;
        }

        [DisplayName("Нижняя граница облачности")]
        public string? CloudBase
        {
            get => _cloudBase;
            set => _cloudBase = value;
        }

        [DisplayName("Горизональная видимость")]
        public string? HorizontalVisibility
        {
            get => _horizontalVisibility; 
            set => _horizontalVisibility = value;
        }

        [DisplayName("Погодные явления")]
        public string? WeatherConditions
        {
            get => _weatherConditions; 
            set => _weatherConditions = value;
        }

        #endregion

    }
}
