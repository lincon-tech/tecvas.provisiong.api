using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Techrunch.TecVas.Provisioning.Api.Helpers.Swagger
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerOptions>
    {
        private readonly SwaggerSettings settings;

        /// <inheritdoc />
        public ConfigureSwaggerOptions(IOptions<SwaggerSettings> settings)
        {
            this.settings = settings?.Value ?? new SwaggerSettings();
        }

        /// <inheritdoc />
        public void Configure(SwaggerOptions options)
        {
            options.RouteTemplate = settings.RoutePrefixWithSlash + "{documentName}/swagger.json";
        }
    }
}
