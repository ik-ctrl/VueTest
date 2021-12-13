using System.ComponentModel;

namespace SimpleBackend.WebApi.Models.Worker
{
    /// <summary>
    /// Тип работы
    /// </summary>
    public enum JobType
    {
        /// <summary>
        /// Работа связанная со списком дел
        /// </summary>
        [Description("Работа с со списком дел")]
        TaskList=1,
        
        /// <summary>
        /// Работа связанная c магазином
        /// </summary>
        [Description("Работа с со списком дел")]
        Store=2,
         
        /// <summary>
        /// Тестовый список дел
        /// </summary>
        [Description("Тестовая работа")]
        AnotherTest=3,
    }
}