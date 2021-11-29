using EnergyHeatMap.Contracts.Models;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Commands
{
    public class UpdateUserCommand : IRequest<IUser>
    {
        public UpdateUserCommand(Guid id, string username)
        {
            Id = id;
            Username = username;
        }

        public Guid Id { get; }
        public string Username { get; }
    }
}
