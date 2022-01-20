using System.Collections.Generic;

namespace SimpleBackend.WebApi.Requests
{
    /// <summary>
    /// Запрос с ключами объектов
    /// </summary>
    public sealed class KeysRequest
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        public KeysRequest() => UiKeys = new List<int>();

        /// <summary>
        /// Идентификаторы которые необходимо удалить
        /// </summary>
        public IEnumerable<int> UiKeys { get; set; }
    }
}