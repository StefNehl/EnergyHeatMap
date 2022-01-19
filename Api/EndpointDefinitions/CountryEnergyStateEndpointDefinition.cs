using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Domain.Enums;
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
            app.MapGet("/energystatevaluetypes/", GetEnergyStateValueTypes);
            app.MapGet("/energystatedata/", GetEnergyStateDataByType);
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

        [Authorize(Roles = $"{Role.User},{Role.Admin}")]
        private IResult GetEnergyStateValueTypes([FromServices] IMediator mediator)
        {
            var types = EnergyStateValueTypesExtensions.GetValues();
            return Results.Ok(types);
        }

        [Authorize(Roles = $"{Role.User},{Role.Admin}")]
        private async Task<IResult> GetEnergyStateDataByType([FromServices] IMediator mediator, 
            string countries, 
            string types,
            DateTime startdate,
            DateTime enddate)
        {
            var countriesArray = countries.Split(',');
            var typesArray = types.Split(',');

            var query = new GetFilteredEnergyStateDataQuery(countriesArray, typesArray, startdate, enddate);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }

    }
}
