using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries;
using EnergyHeatMap.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyHeatMap.Api.EndpointDefinitions
{
    public class CountryEnergyStateEndpointDefinition : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/countries/", GetCountries);
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddSingleton<ICountryEnergyStateServices, CountryEnergyStateService>();
        }

        [Authorize(Roles = $"{Role.User},{Role.Admin}")]
        private async Task<IResult> GetCountries([FromServices] IMediator mediator)
        {
            var query = new GetCountriesQuery();
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }

    }
}
