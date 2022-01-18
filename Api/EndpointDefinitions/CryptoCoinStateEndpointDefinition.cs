﻿using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Domain;
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
            app.MapGet("/cryoticoinstatesfilteredvalue/", GetFilteredValueAsync);
            app.MapGet("/cryoticoinstatesfilteredhashrate/", GetFilteredHashrateAsync);
            app.MapGet("/cryoticoinstatesfiltereddifficulty/", GetFilteredDifficultyAsync);
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

            var typeEnums = new List<CryptoValueTypes>();

            if (typesString.Length == 0)
                typeEnums.Add(CryptoValueTypes.All);
            else
            {
                foreach(var typeString in typesString)
                {
                    if(Enum.TryParse(typeString, out CryptoValueTypes type))
                    {
                        typeEnums.Add(type);
                    }
                }
            }


            var query = new GetFilteredCryptoCoinStatesQuery(coins, startdate, enddate);
            var result = await mediator.Send(query);

            var resultsByType = new List<Tuple<DateTime, decimal, string, string>>();

            foreach (var type in typeEnums)
            {
                switch (type)
                {
                    case CryptoValueTypes.All:
                        return Results.Ok(result);
                    case CryptoValueTypes.Value:
                        resultsByType.AddRange(result
                            .Select(x => new Tuple<DateTime, decimal, string, string>(
                                x.DateTime, x.Value, x.CoinName, CryptoValueTypes.Value.ToString())));
                        break;
                    case CryptoValueTypes.Hashrate:
                        resultsByType.AddRange(result
                            .Select(x => new Tuple<DateTime, decimal, string, string>(
                                x.DateTime, x.Hashrate, x.CoinName, CryptoValueTypes.Hashrate.ToString())));
                        break;
                    case CryptoValueTypes.Difficulty:
                        resultsByType.AddRange(result
                            .Select(x => new Tuple<DateTime, decimal, string, string>(
                                x.DateTime, x.Difficulty, x.CoinName, CryptoValueTypes.Difficulty.ToString())));
                        break;
                    default:
                        return Results.Ok(result);

                }
            }

            return Results.Ok(resultsByType);
        }

        [Authorize(Roles = $"{Role.User}, {Role.Admin}")]
        private async Task<IResult> GetFilteredHashrateAsync([FromServices] IMediator mediator,
            string coinnames,
            DateTime startdate,
            DateTime enddate)
        {
            var coins = coinnames.Split(',');

            var query = new GetFilteredCryptoCoinStatesQuery(coins, startdate, enddate);
            var filteredData = await mediator.Send(query);
            var result = filteredData.Select(x => new Tuple<DateTime, decimal>(x.DateTime, x.Hashrate));

            return Results.Ok(result);
        }

        [Authorize(Roles = $"{Role.User}, {Role.Admin}")]
        private async Task<IResult> GetFilteredDifficultyAsync([FromServices] IMediator mediator,
            string coinnames,
            DateTime startdate,
            DateTime enddate)
        {
            var coins = coinnames.Split(',');

            var query = new GetFilteredCryptoCoinStatesQuery(coins, startdate, enddate);
            var filteredData = await mediator.Send(query);
            var result = filteredData.Select(x => new Tuple<DateTime, decimal>(x.DateTime, x.Difficulty));

            return Results.Ok(result);
        }

        [Authorize(Roles = $"{Role.User}, {Role.Admin}")]
        private async Task<IResult> GetFilteredValueAsync([FromServices] IMediator mediator,
            string coinnames,
            DateTime startdate,
            DateTime enddate)
        {
            var coins = coinnames.Split(',');

            var query = new GetFilteredCryptoCoinStatesQuery(coins, startdate, enddate);
            var filteredData = await mediator.Send(query);
            var result = filteredData.Select(x => new Tuple<DateTime, decimal>(x.DateTime, x.Value));

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
