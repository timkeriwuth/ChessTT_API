using Labo.IL.Configurations;
using Labo.IL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace Labo.IL.DependencyExtensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, JwtConfiguration config)
        {
            services.AddSingleton(config);
            services.AddScoped<JwtSecurityTokenHandler>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
