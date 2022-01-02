using System;
using SimpleBackend.WebApi.Models.Worker;

namespace SimpleBackend.WebApi.Models.Jobs
{
    /// <summary>
    /// Выполняемая работа
    /// </summary>
    public sealed class Job
    {
        /// <summary>
        /// Идентификатор работы
        /// </summary>
        public Guid JobId { get; set; }
        
        /// <summary>
        /// Тип выполняемой работы
        /// </summary>
        public JobType Type { get; set; }

        /// <summary>
        /// Сообщение задачи
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Объект выполения задачи 
        /// </summary>
        public object JobObject { get; set; }

        /// <summary>
        /// Вывод информации о работе
        /// </summary>
        /// <returns>Информации о работе</returns>
        public override string ToString() => $"JobId:{JobId}-JobType:{Type}";
    }
}