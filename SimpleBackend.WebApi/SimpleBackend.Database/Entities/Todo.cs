using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SimpleBackend.Database.Entities
{
    /// <summary>
    /// Задача
    /// </summary>
    [Comment("Основные задачи")]
    public class Todo
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        public Todo()
        {
            SubTodos = new HashSet<SubTodo>();
        }
        
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public int TodoId { get; set; }
        
        /// <summary>
        /// Индентификатор выданный графической системой
        /// </summary>
        public int  UiKey { get; set; }
        
        /// <summary>
        /// Название задачи
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Флаг выполнения задачи
        /// </summary>
        public bool  Confirm { get; set; }
        
        /// <summary>
        /// Список подзадач
        /// </summary>
        public ICollection<SubTodo> SubTodos { get; set; }
    }
}