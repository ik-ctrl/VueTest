using Microsoft.EntityFrameworkCore;

namespace SimpleBackend.Database.Entities
{
    /// <summary>
    /// Подзадача
    /// </summary>
    [Comment("Подзадачи главных задач")]
    public class SubTodo
    {
        /// <summary>
        /// Идентификатор подзадачи
        /// </summary>
        public int SubTodoId { get; set; }
        
        /// <summary>
        /// Индентификатор выданный графической системой
        /// </summary>
        public int  UiKey { get; set; }
        
        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Флаг выполнения задачи
        /// </summary>
        public bool Confirm { get; set; }
        
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public int TodoId { get; set; }
        
        /// <summary>
        /// Навигационное свойство задачи
        /// </summary>
        public Todo Todo { get; set; }
    }
}