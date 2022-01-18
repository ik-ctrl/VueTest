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
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            var connection = ExtractConnectionString(Configuration);
            services.AddDbContext<PostgresDbContext>(options =>
            {
                options.UseNpgsql(connection.GetConnectionString());
            });
            services
                .AddHealthChecks()
                .AddNpgSql(connection.GetConnectionString());

            services.AddSingleton<AcceptedJobQueue>();
            services.AddSingleton<ResultJobQueue>();
            services.AddHostedService<WorkerHostedService>();
            services.AddTransient<JobDispatcherService>();
        }

        /// <summary>
        /// Конфигурация конвейера обработки запроса
        /// </summary>
        /// <param name="app">Конфигуратор приложения</param>
        /// <param name="env">Окружение приложения</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleBackend.WebApi v1"));
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("ApiHealth");
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
        
    }
}