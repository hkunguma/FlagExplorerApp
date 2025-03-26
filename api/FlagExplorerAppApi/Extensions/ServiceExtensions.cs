using FlagExplorerApp.Application.Contracts.Interfaces;
using FlagExplorerApp.Application.Services;
using FlagExplorerApp.Common.Contracts;
using FlagExplorerApp.Common.Helpers;
using FlagExplorerApp.Infrastructure.Services;

namespace FlagExplorerAppApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCountryService(this IServiceCollection services)
        {
            services.AddScoped<ICountryService, CountryService>();
            return services;
        }

        public static IServiceCollection AddRestCountryService(this IServiceCollection services)
        {
            services.AddScoped<IRestCountryService, RestCountryService>();
            return services;
        }

        public static IServiceCollection AddHttpClientHelper(this IServiceCollection services)
        {
            services.AddScoped(typeof(IHttpClient<>), typeof(HttpClientHelper<>));
            return services;
        }

    }
}
