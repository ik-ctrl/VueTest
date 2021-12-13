﻿using System.Collections.Generic;

namespace SimpleBackend.Database.Entities
{
    /// <summary>
    /// Задача
    /// </summary>
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
        public uint TodoId { get; set; }
        
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