using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SimpleBackend.WebApi.Options
{
    /// <summary>
    /// Конфигуратор опций Swagger
    /// </summary>
    internal sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="provider">Провайдер версионности</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
            => _provider = provider;

        /// <summary>
        /// Конфигурирование
        /// </summary>
        /// <param name="options">Опции swagger</param>
        public void Configure(SwaggerGenOptions options)
        {
            var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc( description.GroupName,
                    new OpenApiInfo()
                    {
                        Title = $"SimpleBackend.WebApi v{description.ApiVersion}",
                        Version = description.ApiVersion.ToString()
                    });
                options.IncludeXmlComments(xmlPath);
            }
        }
    }
}