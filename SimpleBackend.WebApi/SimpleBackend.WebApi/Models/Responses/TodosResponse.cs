using System.Collections.Generic;
using SimpleBackend.WebApi.Models.DTO;
using SimpleBackend.WebApi.Models.Enums;

namespace SimpleBackend.WebApi.Models.Responses
{
    /// <summary>
    /// Ответ с набором задач
    /// </summary>
    public class TodosResponse : AbsInformationResponse
    {
        /// <summary>
        /// Инициализация с пустого экземпляра
        /// </summary>
        public TodosResponse()
        {
            Todos = new List<TodoDTO>();
        }

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="todos">Список задач</param>
        /// <param name="errorCode">Код ошибки</param>
        /// <param name="errorMessage"></param>
        public TodosResponse(IEnumerable<TodoDTO> todos, ErrorCodeType errorCode, string errorMessage = "") : base(errorCode, errorMessage) => Todos = todos;

        /// <summary>
        /// Список задач 
        /// </summary>
        public IEnumerable<TodoDTO> Todos { get; init; }
    }
}