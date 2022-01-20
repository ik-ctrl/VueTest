namespace SimpleBackend.WebApi.DTO
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
    }
}