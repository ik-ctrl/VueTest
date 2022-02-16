using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace SimpleBackend.WebApi.Helpers.Extensions
{
    /// <summary>
    /// Расширения для конфигурации линии 
    /// </summary>
    public static class AppBuilderExtension
    {
        /// <summary>
        /// Применение миграциий для базы данных
        /// </summary>
        /// <param name="appBuilder">Конструктор приложения</param>
        /// <param name="factory">Фабрика создания контекста БД</param>
        /// <returns>Сконфигурированный контекст приложения</returns>
        public static IApplicationBuilder ApplyMigration(this IApplicationBuilder appBuilder,PostgresDbContextFactory factory)
        {
            using (var context = factory.CreateDbContext())
            {
                context.Database.Migrate();
            }
            return appBuilder;
        }
        
        /// <summary>
        /// Конфигурация OpenApi
        /// </summary>
        /// <param name="appBuilder">Конструктор приложения</param>
        /// <param name="url">Адрес OpenApi</param>
        /// <param name="name">Фабрика создания контекста БД</param>
        /// <returns>Сконфигурированный контекст приложения</returns>
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder appBuilder,string url, string name)
        {
            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUI(c => c.SwaggerEndpoint(url, name));
            return appBuilder;
        }
        
    }
}