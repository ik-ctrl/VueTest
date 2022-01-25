using System.Collections.Generic;

namespace SimpleBackend.WebApi.DTO
{
    /// <summary>
    /// Запрос с подзадача 
    /// </summary>
    public sealed class SubTodoRequestDTO
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        public SubTodoRequestDTO() => SubTodos = new List<SubTodoDTO>();
        
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