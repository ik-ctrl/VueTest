using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.Extensions;
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
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="errorCode"></param>
        /// <param name="jobId"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private JobInfoResponse GenerateResponse(string location, ErrorCodeType errorCode, Guid jobId, string errorMessage = null)
            => new JobInfoResponse()
            {
                Location = location,
                ErrorCode = errorCode,
                ErrorMessage = string.IsNullOrEmpty(errorMessage) ? errorCode.GetAttribute<DisplayAttribute>().ToString() : errorMessage,
                JobId = jobId
            };

        /// <summary>
        /// Генерация успешного ответа на запрос
        /// </summary>
        /// <param name="location">Информация о дальнейшем расположение</param>
        /// <param name="jobId">Идентификатор работы</param>
        /// <returns>Успешный результат выполнения</returns>
        public JobInfoResponse GenerateSuccessfulResponse(string location, Guid jobId)
            => GenerateResponse(location, ErrorCodeType.NoError, jobId);

            //todo: заменить  "безуспешного"  на что то другое
        /// <summary>
        /// Генерация безуспешного ответа на запрос
        /// </summary>
        /// <param name="errorCode">Код ошибки</param>
        /// <param name="errorMessage">Дополнительная информация об ошибке</param>
        /// <returns>Безуспешный ответ на запрос</returns>
        public JobInfoResponse GenerateUnSuccessfulResponse(ErrorCodeType errorCode, string errorMessage = null)
            => GenerateResponse(string.Empty, errorCode, Guid.Empty, errorMessage);
    }
}