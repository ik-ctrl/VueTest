using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name="")]
        NoError=0,
        
        /// <summary>
        /// Неизвестная ошибка
        /// </summary>
        [Display(Name="Неизвестная ошибка")]
        UnknownError=1,
    }
}