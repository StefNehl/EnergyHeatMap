using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EnergyHeatMap.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
