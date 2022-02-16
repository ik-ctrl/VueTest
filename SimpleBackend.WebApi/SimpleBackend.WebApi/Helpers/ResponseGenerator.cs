using System;
using SimpleBackend.WebApi.DTO;
using SimpleBackend.WebApi.Helpers.Extensions;
using SimpleBackend.WebApi.Models.Enums;

namespace SimpleBackend.WebApi.Helpers
{
    /// <summary>
    /// Генератор ответов сервера
    /// </summary>
    public sealed class ResponseGenerator
    {
        /// <summary>
        /// Генерация ответа при успешном запросе
        /// </summary>
        /// <param name="location">Информация о дальнейшем расположение</param>
        /// <param name="jobId">Идентификатор работы</param>
        /// <returns>Успешный результат выполнения</returns>
        public JobInfoDTO GenerateSuccessfulResponse(string location, Guid jobId)
            => GenerateJobInfoDTO(location, ErrorCodeType.NoError, jobId);
        
        /// <summary>
        /// Генерация ответа при неуспешном запросе
        /// </summary>
        /// <param name="errorCode">Код ошибки</param>
        /// <param name="errorMessage">Дополнительная информация об ошибке</param>
        /// <returns>Безуспешный ответ на запрос</returns>
        public JobInfoDTO GenerateUnSuccessfulResponse(ErrorCodeType errorCode, string errorMessage = null)
            => GenerateJobInfoDTO(string.Empty, errorCode, Guid.Empty, errorMessage);
        
        /// <summary>
        /// Генератор ответов
        /// </summary>
        /// <param name="location">Путь следующего расположения ответа</param>
        /// <param name="errorCode">Код ошибки</param>
        /// <param name="jobId">Идентификатор работы</param>
        /// <param name="errorMessage">Дополнительное сообщение об ошибки</param>
        /// <returns>Ответ об информации о единицах работы </returns>
        private JobInfoDTO GenerateJobInfoDTO(string location, ErrorCodeType errorCode, Guid jobId, string errorMessage = null)
            => new JobInfoDTO()
            {
                Location = location,
                ErrorCode = errorCode,
                ErrorMessage = string.IsNullOrEmpty(errorMessage) ? errorCode.GetName(): errorMessage,
                JobId = jobId
            };

        /// <summary>
        /// Генерация ответа с информацией о статусе принятой работы
        /// </summary>
        /// <param name="location">Путь следующего запроса</param>
        /// <param name="jobId">Идентификатор отслеживаемой работы </param>
        /// <returns>Ответ со статусом принятой работы</returns>
        public JobStatusDTO GenerateAcceptedJobStatusResponse(string location, Guid jobId)
            => GenerateJobStatusDTO(location, jobId, ErrorCodeType.NoError, JobStatusType.Accepted);
        
        /// <summary>
        /// Генерация ответа с информацией о статусе законченной работы
        /// </summary>
        /// <param name="location">Путь следующего запроса</param>
        /// <param name="jobId">Идентификатор отслеживаемой работы </param>
        /// <returns>Ответ со статусом законченной работы</returns>
        public JobStatusDTO GenerateFinishedJobStatusResponse(string location, Guid jobId)
            => GenerateJobStatusDTO(location, jobId, ErrorCodeType.NoError, JobStatusType.Finished);
        
        /// <summary>
        /// Генерация ответа с информацией о статусе не найденной работы
        /// </summary>
        /// <param name="location">Путь следующего запроса</param>
        /// <param name="jobId">Идентификатор отслеживаемой работы </param>
        /// <returns>Ответ со статусом не найденной работы</returns>
        public JobStatusDTO GenerateNotFoundJobStatusResponse(string location, Guid jobId)
            => GenerateJobStatusDTO(location, jobId, ErrorCodeType.NoError, JobStatusType.NotFound);
        
        /// <summary>
        /// Генерация статусов проверки задач
        /// </summary>
        /// <param name="location">Путь следующего запроса</param>
        /// <param name="jobId">Идентификатор работы</param>
        /// <param name="errorCode">Код ошибки</param>
        /// <param name="jobStatus">Статус работы</param>
        /// <param name="statusMessage">Сообщение о статусе</param>
        /// <param name="errorMessage">Дополнительное сообщение о ошибке</param>
        /// <returns></returns>
        private JobStatusDTO GenerateJobStatusDTO(string location, Guid jobId, ErrorCodeType errorCode, JobStatusType jobStatus, string statusMessage = null, string errorMessage = null)
            => new JobStatusDTO()
            {
                Location = location,
                JobId = jobId,
                StatusCode = jobStatus,
                StatusMessage = string.IsNullOrEmpty(statusMessage) ? jobStatus.GetName() : statusMessage,
                ErrorCode = errorCode,
                ErrorMessage = string.IsNullOrEmpty(errorMessage) ? errorCode.GetName() : statusMessage,
            };



    }
}