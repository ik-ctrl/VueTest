using System;

namespace SimpleBackend.WebApi.DTO
{
    /// <summary>
    /// Ответ с информацией о размещенной задаче работе
    /// </summary>
    public class JobInfoDTO:AbsInfoDTO
    {
        /// <summary>
        /// Идентификатор работы
        /// </summary>
        public Guid JobId { get; init; }
        
        /// <summary>
        /// Информация о том с информацией о следующем запросе
        /// </summary>
        public string Location { get; init; }
    }
}