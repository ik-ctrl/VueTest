using SimpleBackend.WebApi.Models.Enums;

namespace SimpleBackend.WebApi.Models.Responses
{
    /// <summary>
    /// Сущность информации о запросе
    /// </summary>
    public abstract class AbsInformationResponse
    {
        /// <summary>
        /// Инициализация пустого экземпляра
        /// </summary>
        protected AbsInformationResponse() { }

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="errorCode">Код ошибки</param>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        protected AbsInformationResponse(ErrorCodeType errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Код ошибки
        /// </summary>
        public ErrorCodeType ErrorCode { get; init; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; init; }
    }
}