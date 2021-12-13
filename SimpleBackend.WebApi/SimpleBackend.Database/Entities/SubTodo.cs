namespace SimpleBackend.Database.Entities
{
    /// <summary>
    /// Подзадача
    /// </summary>
    public class SubTodo
    {
        /// <summary>
        /// Идентификатор подзадачи
        /// </summary>
        public uint SubTodoId { get; set; }
        
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
        public uint TodoId { get; set; }
        
        /// <summary>
        /// Навигационное свойство заданчи
        /// </summary>
        public Todo Todo { get; set; }
    }
}