namespace SimpleBackend.WebApi.DTO
{
    /// <summary>
    /// Сущность для передачи ключей по подзадачам
    /// </summary>
    public class SubTodoUiKeyDTO
    {
        /// <summary>
        /// Графический идентификатор главной задачи
        /// </summary>
        public int TodoUiKey { get; set; }
        /// <summary>
        /// Список ключей для удаления
        /// </summary>
        public int UiKeyItem { get; init; }
    }
}