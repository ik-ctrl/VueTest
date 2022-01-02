using System.ComponentModel;

namespace SimpleBackend.WebApi.Models.Worker
{
    /// <summary>
    /// Тип работы
    /// </summary>
    public enum JobType
    {
        /// <summary>
        /// Добавить новую задачу
        /// </summary>
        [Description("Добавить новую задачу")]
        AddTodo = 1,
        
        /// <summary>
        /// Удалить задачу
        /// </summary>
        [Description("Удалить задачу")]
        RemoveTodo=2,
        
        /// <summary>
        /// Обновить задачу
        /// </summary>
        [Description("Обновить задачу")]
        UpdateTodos=3,
        
        /// <summary>
        /// Получить все задачи
        /// </summary>
        [Description("Получить все задачи")]
        GetAllTodos=4,
        
        /// <summary>
        /// Добавить подзадачи
        /// </summary>
        [Description("Добавить подзадачи")]
        AddSubTodos=5,

        /// <summary>
        /// Удалить подзадачи
        /// </summary>
        [Description("Удалить подзадачи")]
        RemoveSubTodos=6,

        /// <summary>
        /// Обновить подзадачи
        /// </summary>
        [Description("Обновить подзадачи")]
        UpdateSubTodos=7,
    }
}