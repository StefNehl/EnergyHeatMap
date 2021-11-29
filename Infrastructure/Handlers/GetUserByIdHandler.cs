using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, IUser?>
    {
        private readonly IUsersService _userRepo;

        public GetUserByIdHandler(IUsersService userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IUser?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepo.GetById(request.Id);
        }
    }
}
