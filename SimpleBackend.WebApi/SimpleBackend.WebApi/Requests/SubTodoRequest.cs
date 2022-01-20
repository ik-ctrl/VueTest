using System.Collections.Generic;
using SimpleBackend.WebApi.DTO;

namespace SimpleBackend.WebApi.Requests
{
    /// <summary>
    /// Запрос с подзадача 
    /// </summary>
    public sealed class SubTodoRequest
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        public SubTodoRequest() => SubTodos = new List<SubTodoDTO>();
        
        /// <summary>
        /// Графический идентификатор задачки
        /// </summary>
        public int TodoUiKey { get; init; } 
        
        /// <summary>
        /// Список данных подзадач
        /// </summary>
        public IEnumerable<SubTodoDTO> SubTodos { get; init; }
        
    }
}