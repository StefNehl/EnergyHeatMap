using MediatR;
using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Handlers
{
    internal class AuthenticateHandler : IRequestHandler<AuthenticateCommand, IUser>
    {
        private readonly IUsersService _userService;

        public AuthenticateHandler(IUsersService usersService)
        {
            _userService = usersService;
        }

        public async Task<IUser> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return await _userService.AuthenticateAsync(request.Username, request.Password, cancellationToken);
        }
    }
}
