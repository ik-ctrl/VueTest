namespace SimpleBackend.WebApi.Models
{
    /// <summary>
    /// Данные для подключения к БД
    /// </summary>
    public class PostgresConnection
    {
        /// <summary>
        /// Наименование БД
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Хост
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string  Username { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Порт подключения
        /// </summary>
        public uint Port { get; set; }

        /// <summary>
        /// Время жизни простающего подключения при привышении размера минимального пула подключений
        /// </summary>
        public uint ConnectionIdleLifetime { get; set; }
        
        /// <summary>
        /// Ожидание пула перед отсечением простаювающих подключений (todo:дописать)
        /// </summary>
        public  uint  ConnectionPruningInterval { get; set; }
    }
}