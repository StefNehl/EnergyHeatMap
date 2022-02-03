using EnergyHeatMap.Contracts.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Queries.HeatMap
{
    public class GetHeatMapValueTypesQuery : IRequest<IEnumerable<IHeatMapValueType>>
    {
    }
}
