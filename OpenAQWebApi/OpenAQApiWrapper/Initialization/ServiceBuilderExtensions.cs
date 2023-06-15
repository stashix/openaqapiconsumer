using Microsoft.Extensions.DependencyInjection;
using OpenAQApiWrapper.Services;
using System.Net.Http.Headers;

namespace OpenAQApiWrapper.Initialization
{
    public static class ServiceBuilderExtensions
    {
        public static IServiceCollection AddOpenAQ(this IServiceCollection services)
        {
            services.AddTransient<IQuerySerializer, ReflectionQuerySerializer>();
            services.AddHttpClient<IOpenAQApiWrapper, OpenAQV2ApiWrapper>(client =>
            {
                client.BaseAddress = new Uri("https://api.openaq.org/v2/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            return services;
        }
    }
}
