using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SimpleBackend.WebApi.Options
{
    
    internal class ConfigureSwaggerOptions:IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }
        
        public void Configure(SwaggerGenOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}