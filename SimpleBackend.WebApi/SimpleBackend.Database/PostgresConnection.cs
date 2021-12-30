namespace SimpleBackend.Database
{
    /// <summary>
    /// Данные соединения с БД
    /// </summary>
    public sealed record PostgresConnection
    {
        /// <summary>
        /// Хост БД
        /// </summary>
        public string Host { get; init; } 

        /// <summary>
        ///  Порт
        /// </summary>
        public uint Port { get; init; }

        /// <summary>
        /// Наименование БД
        /// </summary>
        public string DatabaseName { get; init; }

        /// <summary>
        /// Пользователь БД
        /// </summary>
        public string Username { get; init; }

        /// <summary>
        /// Пароль от БД
        /// </summary>
        public string Password { get; init; }

        /// <summary>
        /// Время жизни idle подключения после если превышается минимальное допустимое значений подключений
        /// </summary>
        public uint ConnectionIdleLifetime { get; init; }

        /// <summary>
        /// Время ожидания перед отключением idle подключений
        /// </summary>
        public uint ConnectionPruningInterval { get; init; }

        /// <summary>
        /// Формирования строки подключения к БД
        /// </summary>
        /// <returns>Строка подключения к БД</returns>
        public string GetConnectionString()=> $"Host={Host};Port={Port};Username={Username};Password={Password};Database={DatabaseName};" +
                                              $"Connection Idle Lifetime={ConnectionIdleLifetime};ConnectionPruningInterval={ConnectionPruningInterval}";
        // public string GetConnectionString() => $"Host={Host};Port={Port};Username={Username};Password={Password};Database={DatabaseName};ApplicationName={AppName}";
    }
}