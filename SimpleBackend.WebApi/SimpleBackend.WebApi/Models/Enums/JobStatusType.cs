using System.ComponentModel.DataAnnotations;

namespace SimpleBackend.WebApi.Models.Enums
{
    /// <summary>
    /// Статус выполненой работы
    /// </summary>
    public enum JobStatusType
    {
        /// <summary>
        /// Задача принята в обработку
        /// </summary>
        [Display(Name = "Задача принята в обработку")]
        Accepted = 0,

        /// <summary>
        /// Задача выполнена
        /// </summary>
        [Display(Name = "Задача выполнена")]
        Finished = 1,
        
        /// <summary>
        /// Задача не найдена в очередях
        /// </summary>
        [Display(Name = "Задача не найдена в очередях")]
        NotFound = 2,
    }
}