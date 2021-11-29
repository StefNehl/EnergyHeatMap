using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using EnergyHeatMap.Infrastructure.Commands;
using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;

namespace EnergyHeatMap.Infrastructure.Handlers
{
    internal class CreateUserHandler : IRequestHandler<CreateUserCommand, IUser>
    {
        private readonly IUsersService _userRepo;

        public CreateUserHandler(IUsersService userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IUser> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepo.CreateNewUser(request.Username, request.Password);
        }
    }
}
