using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Origin.Service.Extensions
{
    public static class DocumentProviderExtensions
    {
        /// <summary>
        /// A collection of additional assemblies that should be eager loaded at startup.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddOriginAdditionalAssemblies(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            if (assemblies is null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            // Intentionally returns app without actually doing anything.
            return services;
        }
    }
}
