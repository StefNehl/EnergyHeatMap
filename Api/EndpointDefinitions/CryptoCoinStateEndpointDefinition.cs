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
            app.MapGet("/cryoticoinstates/", GetFilterdAsync);
            app.MapGet("/cryptocoins/", GetCryptoCoins);
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

        [Authorize(Roles = $"{Role.User},{Role.Admin}")]
        private async Task<IResult> GetFilterdAsync([FromServices] IMediator mediator, 
            string coinname, 
            DateTime startdate,
            DateTime enddate)
        {
            var query = new GetFilteredCryptoCoinStatesQuery(coinname, startdate, enddate);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }

        [Authorize(Roles = $"{Role.User},{Role.Admin}")]
        private async Task<IResult> GetCryptoCoins([FromServices] IMediator mediator)
        {
            var query = new GetCryptoCoinsQuery();
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }
    }
}
