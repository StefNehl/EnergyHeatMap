using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Queries.Chart
{
    public class GetCryptoCoinsQuery : IRequest<IEnumerable<string>>
    {
    }
}
