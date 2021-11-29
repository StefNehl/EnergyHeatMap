using EnergyHeatMap.Api.EndpointDefinitions;

namespace EnergyHeatMap.Api.Extensions
{
    public static class EndpointDefinitionExtensions
    {
        public static void AddEndpointDefinitions(this IServiceCollection services, params Type[] scanMarkers)
        {
            var endpointDefinitions = new List<IEndpointDefinition>();

            //TODO check assignable
            foreach (var marker in scanMarkers)
            {
                var exportedTypes = marker.Assembly.ExportedTypes
                        .Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x)).ToList();


                endpointDefinitions.AddRange(exportedTypes
                    .Where(x => x.IsClass || x.IsInterface)
                    .Select(Activator.CreateInstance)
                    .Cast<IEndpointDefinition>());
            }

            foreach (var endpointDefinition in endpointDefinitions)
            {
                endpointDefinition.DefineServices(services);
            }

            services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);
        }

        public static void UseEndpointDefinitions(this WebApplication app)
        {
            var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

            foreach (var endpointDefinition in definitions)
            {
                endpointDefinition.DefineEndpoints(app);
            }
        }


    }
}
