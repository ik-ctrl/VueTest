using System.Collections.Generic;

namespace SimpleBackend.WebApi.DTO
{
    /// <summary>
    /// Модель задачи 
    /// </summary>
    public sealed class TodoDTO
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        public TodoDTO() => SubTodos = new List<SubTodoDTO>();
        
        /// <summary>
        /// Индентификатор выданный графической системой
        /// </summary>
        public int UiId { get; init; }

        /// <summary>
        /// Название задачи
        /// </summary>
        public string Title { get; init; }

        /// <summary>
        /// Флаг выполнения задачи
        /// </summary>
        public bool Confirm { get; init; }

        /// <summary>
        /// Список подзадач
        /// </summary>
        public IEnumerable<SubTodoDTO> SubTodos { get; init; }
    }
}