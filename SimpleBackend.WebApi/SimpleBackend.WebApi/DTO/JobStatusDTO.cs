using System;
using SimpleBackend.WebApi.Models.Enums;

namespace SimpleBackend.WebApi.DTO
{
    /// <summary>
    /// Результат проверки статуса работы
    /// </summary>
    public sealed class JobStatusDTO:AbsInfoDTO
    {
        /// <summary>
        /// Идентификатор отслеживаемой работы
        /// </summary>
        public Guid JobId { get; init; }
        
        /// <summary>
        ///  Статус код выполнения работы
        /// </summary>
        public JobStatusType StatusCode { get; init; } 
        
        /// <summary>
        /// Информация о статусе работы
        /// </summary>
        public string StatusMessage { get; init; } 
        
        /// <summary>
        /// Место расположение ответа
        /// </summary>
        public string Location { get; init; }
    }
}