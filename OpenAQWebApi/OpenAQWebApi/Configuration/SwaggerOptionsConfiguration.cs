using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OpenAQWebApi.Configuration
{
    public class SwaggerOptionsConfiguration : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

        public SwaggerOptionsConfiguration(
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            _apiVersionDescriptionProvider = apiVersionDescriptionProvider 
                ?? throw new ArgumentNullException(nameof(apiVersionDescriptionProvider));
        }

        public void Configure(string? name, SwaggerGenOptions options) => Configure(options);

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo()
                    {
                        Title = "OpenAQ Api",
                        Version = description.ApiVersion.ToString()
                    });
            }
        }
    }
}
