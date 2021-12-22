using System.Collections.Generic;

namespace SimpleBackend.WebApi.Models.DTO
{
    /// <summary>
    /// Модель задачи 
    /// </summary>
    public class TodoDTO
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        public TodoDTO() => SubTodos = new List<SubTodoDTO>();

        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public uint TodoId { get; init; }

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