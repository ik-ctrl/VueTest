namespace SimpleBackend.WebApi.DTO
{
    /// <summary>
    /// Сообщение для клиента
    /// </summary>
    public sealed class ServerMessage
    {
        /// <summary>
        /// Сообщения для пользователя
        /// </summary>
        public string Message { get; init; }
    }
}