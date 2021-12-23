using System.ComponentModel;

namespace SimpleBackend.WebApi.Models.Enums
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public enum ErrorCodeType
    {
        /// <summary>
        /// Запрос выполнился без ошибок
        /// </summary>
        [Description("")]
        NoError=0,
        
        /// <summary>
        /// Неизвестная ошибка
        /// </summary>
        [Description("Неизвестная ошибка")]
        UnknownError=1,
    }
}