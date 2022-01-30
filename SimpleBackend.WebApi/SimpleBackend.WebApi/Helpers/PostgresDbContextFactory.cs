using System;
using SimpleBackend.Database;

namespace SimpleBackend.WebApi.Helpers
{
    /// <summary>
    /// Фабрика создания контекста базы данных
    /// </summary>
    public class PostgresDbContextFactory
    {
        private readonly PostgresConnection _connection;

        /// <summary>
        ///  Инициализация
        /// </summary>
        /// <param name="connection">Данные подключения к БД</param>
        /// <exception cref="ArgumentNullException">connection=null</exception>
        public PostgresDbContextFactory(PostgresConnection connection)
            => _connection = connection??throw new ArgumentNullException(nameof(connection),"Отсутствует данные для подключения к БД");
        
        /// <summary>
        /// Создания контекста базы данных
        /// </summary>
        /// <returns> Контекст БД</returns>
        public PostgresDbContext CreateDbContext() => new PostgresDbContext(_connection);
    }
}