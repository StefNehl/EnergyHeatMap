using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EnergyHeatMap.Api.EndpointDefinitions
{
    interface IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app);
        public void DefineServices(IServiceCollection services);
    }
}
