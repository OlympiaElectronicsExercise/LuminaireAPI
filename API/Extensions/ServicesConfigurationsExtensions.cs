using API.Interfaces;
using API.Services;

namespace API.Extensions
{
    public static class ServicesConfigurationsExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>(); 
            // services.AddTransient<ITokenService, TokenService>();
            // services.AddSingleton<ITokenService, TokenService>();
            return services;
        }
    }

}