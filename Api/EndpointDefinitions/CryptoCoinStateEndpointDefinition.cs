using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Domain;
using EnergyHeatMap.Domain.Models;
using EnergyHeatMap.Infrastructure.Queries;
using EnergyHeatMap.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EnergyHeatMap.Api.EndpointDefinitions
{
    public class CryptoCoinStateEndpointDefinition : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/cryptocoinstates/", GetAllAsync);
            app.MapGet("/cryoticoinstatesfiltered/", GetFilterdAsync);
            app.MapGet("/cryoticoinstatesfilteredwithtype/", GetFilterdWithTypeAsync);
            app.MapGet("/cryptocoins/", GetCryptoCoins);
            app.MapGet("/cryptovaluetypes/", GetCryptoValueTypes);
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
            string coinnames, 
            DateTime startdate,
            DateTime enddate)
        {
            var coins = coinnames.Split(',');

            var query = new GetFilteredCryptoCoinStatesQuery(coins, startdate, enddate);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }

        [Authorize(Roles = $"{Role.User},{Role.Admin}")]
        private async Task<IResult> GetFilterdWithTypeAsync([FromServices] IMediator mediator,
            string coinnames,
            DateTime startdate,
            DateTime enddate,
            string types)
        {
            var coins = coinnames.Split(',');
            var typesString = types.Split(',');


            var query = new GetFilteredCryptoCoinStatesByTypeQuery(coins, typesString, startdate, enddate);
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

        [Authorize(Roles = $"{Role.User},{Role.Admin}")]
        private IResult GetCryptoValueTypes([FromServices] IMediator mediator)
        {
            var types = Enum.GetValues(typeof(CryptoValueTypes)).Cast<CryptoValueTypes>().Select(x => x.ToString());
            return Results.Ok(types);
        }
    }
}
