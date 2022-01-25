using SimpleBackend.WebApi.DTO;

namespace SimpleBackend.WebApi.Models.Responses
{
    /// <summary>
    /// Ответ с информации об операции
    /// </summary>
    public class ResultResponse:AbsInfoDTO
    {
        /// <summary>
        /// Флаг успешность выполнения операции
        /// </summary>
        public bool isSuccessful { get; init; } 
    }
}