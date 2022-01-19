using System.Collections.Generic;

namespace SimpleBackend.WebApi.DTO
{
    /// <summary>
    /// Модель данных для удаления todo  
    /// </summary>
    public sealed class UiKeysDTO
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        public UiKeysDTO() => UiKeys = new List<int>();
        
        /// <summary>
        /// Список ключей для удаления
        /// </summary>
        public IEnumerable<int> UiKeys { get; init; }
    }
}