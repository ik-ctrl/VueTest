using System;

namespace SimpleBackend.WebApi.DTO
{
    /// <summary>
    /// Пакет с идентификатором работы
    /// </summary>
    public class JobIdDTO
    {
        /// <summary>
        /// Идентификатор работы
        /// </summary>
        public Guid JobId { get; set; }
    }
}