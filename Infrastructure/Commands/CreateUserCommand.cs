using EnergyHeatMap.Contracts.Models;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Commands
{
    public class CreateUserCommand : IRequest<IUser>
    {
        public CreateUserCommand(string username, string password)
        {
            Username = username;
            Password = password;    
        }

        public string Username { get; }
        public string Password { get; }
    }
}
