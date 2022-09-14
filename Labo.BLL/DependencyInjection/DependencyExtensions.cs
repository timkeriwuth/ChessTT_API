using Labo.BLL.Interfaces;
using Labo.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Labo.BLL.DependencyInjection
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IMatchService, MatchService>();
            return services;
        }
    }
}
