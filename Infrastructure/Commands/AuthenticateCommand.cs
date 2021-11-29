using MediatR;
using EnergyHeatMap.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Commands
{
    public class AuthenticateCommand : IRequest<IUser>
    {
        public AuthenticateCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public string Username { get; }

        public string Password { get; }
    }
}
