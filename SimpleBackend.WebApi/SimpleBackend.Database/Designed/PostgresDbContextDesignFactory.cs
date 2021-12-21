using Microsoft.EntityFrameworkCore.Design;

namespace SimpleBackend.Database.Designed
{
    /// <summary>
    /// Фабрика для создания контекста на момент миграции
    /// </summary>
    public class PostgresDbContextDesignFactory : IDesignTimeDbContextFactory<PostgresDbContext>
    {
        /// <summary>
        /// Создания DbContext для применения миграций
        /// </summary>
        /// <param name="args">аругменты командной строки</param>
        /// <returns>Контекст БД</returns>
        public PostgresDbContext CreateDbContext(string[] args)
        {
            var connection = new PostgresConnection()
            {
                AppName = "SimpleBacked.WebApi",
                ConnectionIdleLifetime = 5,
                ConnectionPruningInterval=10,
                DatabaseName = "SimpleDB",
                Host = "127.0.0.1",
                Password = "postgres",
                Port = 5434,
                Username = "postgres"
            };
            return new PostgresDbContext(connection);

        }
    }
}