using System;
using Microsoft.OpenApi.Extensions;
using SimpleBackend.WebApi.Models.Enums;
using SimpleBackend.WebApi.Models.Responses;

namespace SimpleBackend.WebApi.Helpers
{
    /// <summary>
    /// Генератор ответов сервера
    /// </summary>
    public sealed class ResponseGenerator
    {
        private string  GetEnumDescription(ErrorCodeType errorCode)
        {
            var test = errorCode.GetDisplayName();
            return "";
        }

        private JobInfoResponse GenerateResponse(string location, ErrorCodeType errorCode, Guid jobId, string errorMessage = null)
            => new JobInfoResponse()
            {
                Location = location,
                ErrorCode = errorCode,
                ErrorMessage = string.IsNullOrEmpty(errorMessage) ? GetEnumDescription(errorCode) : errorMessage,
                JobId = jobId
            };
    }
}