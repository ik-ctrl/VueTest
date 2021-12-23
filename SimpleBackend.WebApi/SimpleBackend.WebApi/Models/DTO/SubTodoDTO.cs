namespace SimpleBackend.WebApi.Models.DTO
{
    /// <summary>
    /// Модель подзадачи
    /// </summary>
    public class SubTodoDTO
    {
        /// <summary>
        /// Индентификатор выданный графической системой
        /// </summary>
        public int  UiId { get; init; }
        
        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; init; }
        
        /// <summary>
        /// Флаг выполнения задачи
        /// </summary>
        public bool Confirm { get; init; }
        
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public uint TodoId { get; init; }
        
    }
}