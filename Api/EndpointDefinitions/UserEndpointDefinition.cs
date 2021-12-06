using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Commands;
using EnergyHeatMap.Infrastructure.Queries;
using EnergyHeatMap.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EnergyHeatMap.Domain.Models;
using EnergyHeatMap.Contracts.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EnergyHeatMap.Api.EndpointDefinitions
{
    public class UserEndpointDefinition : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/users", GetAll);
            app.MapGet("/users/{id}", GetById);
            app.MapPost("/users", Post);
            app.MapPut("/users", Put);
            app.MapDelete("/users/{id}", Delete);
            app.MapPost("/users/authenticate", AuthenticateAsync).AllowAnonymous();
        }

        [Authorize(Roles = Role.Admin)]
        private async Task<IResult> GetAll([FromServices] IMediator mediator)
        {
            var query = new GetAllUseresQuery();
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }

        [Authorize(Roles = Role.Admin)]
        private async Task<IResult> GetById([FromServices] IMediator mediator, Guid id)
        {
            var query = new GetUserByIdQuery(id);
            var result = await mediator.Send(query);
            return result is not null ? Results.Ok(result) : Results.NotFound();

        }

        [Authorize(Roles = Role.Admin)]
        private async Task<IResult> Delete([FromServices] IUsersService repo, Guid id)
        {
            var result = await repo.DeleteUser(id);

            if (result)
                return Results.Ok();
            return Results.NotFound();
        }

        [Authorize(Roles = Role.Admin)]
        private async Task<IResult> Put([FromServices] IMediator mediator, IUser userToUpdate)
        {
            if (userToUpdate is null)
                return Results.NoContent();

            var command = new UpdateUserCommand(userToUpdate.Id, userToUpdate.Username);
            var result = await mediator.Send(command);

            if (result is null)
                return Results.NotFound();

            return Results.Accepted($"/users/{result.Id}", result);
        }

        [Authorize(Roles = Role.Admin)]
        private async Task<IResult> Post([FromServices] IMediator mediator, string name, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Results.NoContent();

            if (string.IsNullOrWhiteSpace(password))
                return Results.NoContent();

            var command = new CreateUserCommand(name, password);
            var result = await mediator.Send(command);
            return Results.Created($"/users/{result.Id}", result);
        }

        private async Task<IResult> AuthenticateAsync(HttpContext http, [FromServices] IMediator mediator, [FromBody] AuthenticateModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.Username) || string.IsNullOrWhiteSpace(model?.Password))
                return Results.NoContent();

            var query = new AuthenticateCommand(model.Username, model.Password);
            var result = await mediator.Send(query);

            if(result is null)
                return Results.NotFound("Username oder Pw wrong");

            return Results.Ok(result);
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddSingleton<IUsersService, UsersService>();
        }
    }

    public class AuthorizeData : IAuthorizeData
    {
        public string? Policy { get; set; }
        public string? Roles { get; set; }
        public string? AuthenticationSchemes { get; set; }
    }
}
