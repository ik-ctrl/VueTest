using SimpleBackend.WebApi.Models.Enums;

namespace SimpleBackend.WebApi.DTO
{
    /// <summary>
    /// Сущность информации о запросе
    /// </summary>
    public abstract class AbsInfoDTO
    {
        /// <summary>
        /// Инициализация пустого экземпляра
        /// </summary>
        protected AbsInfoDTO() { }

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="errorCode">Код ошибки</param>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        protected AbsInfoDTO(ErrorCodeType errorCode, string errorMessage)
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