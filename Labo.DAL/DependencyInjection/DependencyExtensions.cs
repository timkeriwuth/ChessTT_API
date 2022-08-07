using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ToolBox.EF.Repository;

namespace Labo.DAL.DependencyInjection
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(RepositoryBase)))
                .ToList()
                .ForEach(t => services.AddScoped(t.GetInterfaces().First(i => !i.IsGenericType), t));
            return services;
        }
    }
}
