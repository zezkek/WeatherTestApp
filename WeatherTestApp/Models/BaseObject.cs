using System.ComponentModel.DataAnnotations;

namespace WeatherTestApp.Models
{
    public class BaseObject
    {
        /// <summary>
        /// Базовый класс для объектов
        /// </summary>
        #region Constructor

        public BaseObject() => _guid = Guid.NewGuid();

        #endregion

        #region Fields

        private Guid _guid;

        #endregion

        #region Properties

        [Key]
        public Guid Guid
        {
            get => _guid; 
            set => _guid = value;
        }

        #endregion
    }
}
