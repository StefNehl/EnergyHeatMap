using EnergyHeatMap.Contracts.Models;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Queries
{
    public class GetAllUseresQuery : IRequest<IEnumerable<IUser>>
    {
    }
}
