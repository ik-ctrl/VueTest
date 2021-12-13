using System;
using System.Threading.Tasks;

namespace SimpleBackend.WebApi.Models.Worker
{
    /// <summary>
    /// Выполняемая работа
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Идентефикатор работы
        /// </summary>
        public Guid JobId { get; set; }
        
        /// <summary>
        /// Тип выполняемой работы
        /// </summary>
        public JobType Type { get; set; }

        /// <summary>
        /// Задача, которую необходимо выполнить
        /// </summary>
        public Task Task { get; set; }
    }
}