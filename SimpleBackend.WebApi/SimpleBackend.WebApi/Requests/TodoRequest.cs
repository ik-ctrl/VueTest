using System.Collections.Generic;
using SimpleBackend.WebApi.DTO;

namespace SimpleBackend.WebApi.Requests
{
    /// <summary>
    /// Запрос с задачками
    /// </summary>
    public sealed class TodoRequest
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        public TodoRequest() => Todos = new List<TodoDTO>(); 

        /// <summary>
        /// Список задач
        /// </summary>
        public IEnumerable<TodoDTO> Todos { get; init; }
    }
}