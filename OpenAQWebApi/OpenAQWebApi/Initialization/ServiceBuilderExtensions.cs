using OpenAQWebApi.Services;
using System.Net.Http.Headers;

namespace OpenAQWebApi.Initialization
{
    public static class ServiceBuilderExtensions
    {
        public static IServiceCollection AddOpenAQ(this IServiceCollection services)
        {
            services.AddHttpClient<IOpenAQApiWrapper, OpenAQV2ApiWrapper>(client =>
            {
                client.BaseAddress = new Uri("https://api.openaq.org/v2/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            return services;
        }
    }
}
