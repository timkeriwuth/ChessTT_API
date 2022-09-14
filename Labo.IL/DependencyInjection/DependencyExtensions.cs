using Labo.BLL.Interfaces;
using Labo.IL.Configurations;
using Labo.IL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;

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

        public static IServiceCollection AddMailer(this IServiceCollection services, MailerConfig config)
        {
            services.AddSingleton(config);
            services.AddScoped<SmtpClient>();
            services.AddScoped<IMailer, Mailer>();

            return services;
        }
    }
}
