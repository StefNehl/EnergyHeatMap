using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Commands;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers
{
    public class UpdateOrCreateUserHandler : IRequestHandler<UpdateUserCommand, IUser>
    {
        private readonly IUsersService _userRepo;

        public UpdateOrCreateUserHandler(IUsersService userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IUser> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepo.UpdateUser(request.Id, request.Username);
        }
    }
}
