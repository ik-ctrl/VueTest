using System;
using SimpleBackend.WebApi.Helpers.Extensions;
using SimpleBackend.WebApi.Models.Enums;
using SimpleBackend.WebApi.Models.Responses;

namespace SimpleBackend.WebApi.Helpers
{
    /// <summary>
    /// Генератор ответов сервера
    /// </summary>
    public sealed class ResponseGenerator
    {
        /// <summary>
        /// Генератор ответов
        /// </summary>
        /// <param name="location">Путь следующего расположения ответа</param>
        /// <param name="errorCode">Код ошибки</param>
        /// <param name="jobId">Идентификатор работы</param>
        /// <param name="errorMessage">Дополнительное сообщение об ошибки</param>
        /// <returns>Ответ об информации о единицах работы </returns>
        private JobInfoResponse GenerateResponse(string location, ErrorCodeType errorCode, Guid jobId, string errorMessage = null)
            => new JobInfoResponse()
            {
                Location = location,
                ErrorCode = errorCode,
                ErrorMessage = string.IsNullOrEmpty(errorMessage) ? errorCode.GetName(): errorMessage,
                JobId = jobId
            };

        /// <summary>
        /// Генерация ответа при успешном запросе
        /// </summary>
        /// <param name="location">Информация о дальнейшем расположение</param>
        /// <param name="jobId">Идентификатор работы</param>
        /// <returns>Успешный результат выполнения</returns>
        public JobInfoResponse GenerateSuccessfulResponse(string location, Guid jobId)
            => GenerateResponse(location, ErrorCodeType.NoError, jobId);
        
        /// <summary>
        /// Генерация ответа при неуспешном запросе
        /// </summary>
        /// <param name="errorCode">Код ошибки</param>
        /// <param name="errorMessage">Дополнительная информация об ошибке</param>
        /// <returns>Безуспешный ответ на запрос</returns>
        public JobInfoResponse GenerateUnSuccessfulResponse(ErrorCodeType errorCode, string errorMessage = null)
            => GenerateResponse(string.Empty, errorCode, Guid.Empty, errorMessage);
    }
}