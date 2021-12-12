using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
        /// <param name="configuration">Конифигурация приложения</param>
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
            services.AddApiVersioning();
;           services.AddHealthChecks();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimpleBackend.WebApi", Version = "v1" }); });
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
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}