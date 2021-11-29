using EnergyHeatMap.Contracts.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Queries
{
    public class GetAllCryptoCoinStatesQuery : IRequest<IEnumerable<ICryptoCoinState>>
    {
    }
}
