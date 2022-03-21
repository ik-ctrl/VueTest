using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SimpleBackend.Database;
using SimpleBackend.WebApi.Helpers;
using SimpleBackend.WebApi.Helpers.Extensions;
using SimpleBackend.WebApi.Models;
using SimpleBackend.WebApi.Models.Jobs;
using SimpleBackend.WebApi.Models.Jobs.Storage;
using SimpleBackend.WebApi.Models.Jobs.Worker;
using SimpleBackend.WebApi.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SimpleBackend.WebApi
{
    /// <summary>
    /// Класс запуска приложения
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Инициализация приложения
        /// </summary>
        /// <param name="configuration">Конфигурация приложения</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///Конфигурация приложения
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Конфигурация используемых сервисов
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            services.AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
            var connection = ExtractConnectionString(Configuration);
            services.AddDbContext<PostgresDbContext>(options => { options.UseNpgsql(connection.GetConnectionString()); });
            services.AddTransient<PostgresDbContextFactory>(provider => CreateDbContextFactory(connection));
            services
                .AddHealthChecks()
                .AddNpgSql(connection.GetConnectionString());

            services.AddSingleton<AcceptedJobQueue>();
            services.AddSingleton<ResultJobQueue>();
            services.AddTransient<TodoWorkerService>();
            services.AddTransient<JobGenerator>();
            services.AddTransient<ResponseGenerator>();
            services.AddTransient<JobDispatcherService>();
            services.AddHostedService<WorkerHostedService>();
            services.AddTransient(provider =>CreateTokenGenerator(Configuration));
            services.AddTransient<AuthService>();
        }

        /// <summary>
        /// Конфигурация конвейера обработки запроса
        /// </summary>
        /// <param name="app">Конфигуратор приложения</param>
        /// <param name="env">Окружение приложения</param>
        /// <param name="factory"> Фабрика контекста бд</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,PostgresDbContextFactory factory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.ApplyMigration(factory);
            app.ConfigureSwagger("/swagger/v1/swagger.json", "SimpleBackend.WebApi v1");
            app.UseCors();
            app.UseMiddleware<JWTMiddleware>();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("hc");
            });
        }

        /// <summary>
        /// Извлечение данных для подключения к БД
        /// </summary>
        /// <param name="appConfig">Конфигурация приложения</param>
        /// <returns>Данные для подключения к базе данных</returns>
        private PostgresConnection ExtractConnectionString(IConfiguration appConfig)
        {
            var connectionData = new PostgresConnection();
            appConfig.GetSection(nameof(PostgresConnection)).Bind(connectionData);
            return connectionData;
        }

        /// <summary>
        /// Создание фабрики контекста БД
        /// </summary>
        /// <param name="connection">Данные подключения БД</param>
        /// <returns>Фабрика для создания контекста БД</returns>
        private PostgresDbContextFactory CreateDbContextFactory(PostgresConnection connection) => new PostgresDbContextFactory(connection);

        /// <summary>
        /// Создание генератора токенов
        /// </summary>
        /// <param name="config">Конфигурация приложения</param>
        /// <returns>Сконфигурированный конфигуратор токенов</returns>
        private TokenGenerator CreateTokenGenerator(IConfiguration config)
        {
            var options = new AuthOptions();
            config.GetSection(nameof(AuthOptions)).Bind(options);
            return new TokenGenerator(options);
        }
    }
}