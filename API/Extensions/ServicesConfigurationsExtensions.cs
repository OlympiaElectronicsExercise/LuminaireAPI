using API.Interfaces;
using API.Models;
using API.Services;
using Microsoft.Extensions.Configuration;

namespace API.Extensions
{
    public static class ServicesConfigurationsExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();


            var settings = config.GetSection("Email");
            services.Configure<EmailConfigurationModel>(settings);
            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }

}