using System;
using Microsoft.AspNetCore.Components;

namespace SimpleBackend.WebApi.Models.Responses
{
    /// <summary>
    /// Ответ с информацией о размещенной задаче работе
    /// </summary>
    public class JobInfoResponse:AbsInformationResponse
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