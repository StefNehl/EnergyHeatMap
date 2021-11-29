using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUseresQuery, IEnumerable<IUser>>
    {
        private readonly IUsersService _userRepo;

        public GetAllUsersHandler(IUsersService userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<IUser>> Handle(GetAllUseresQuery request, CancellationToken cancellationToken)
        {
            return await _userRepo.GetAllAsync();
        }
    }
}
