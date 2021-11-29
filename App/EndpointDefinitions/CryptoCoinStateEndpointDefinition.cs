using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Infrastructure.Queries;
using EnergyHeatMap.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyHeatMap.Api.EndpointDefinitions
{
    public class CryptoCoinStateEndpointDefinition : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/cryptocoinstates/", GetAllAsync);
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddSingleton<ICryptoCoinStateService, CryptoCoinStateService>();
        }

        [Authorize(Roles = $"{Role.User},{Role.Admin}")]
        private async Task<IResult> GetAllAsync([FromServices] IMediator mediator)
        {
            var query = new GetAllCryptoCoinStatesQuery();
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }
    }
}
