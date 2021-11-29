using EnergyHeatMap.Contracts.Models;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Queries
{
    public class GetUserByIdQuery : IRequest<IUser>
    {
        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
