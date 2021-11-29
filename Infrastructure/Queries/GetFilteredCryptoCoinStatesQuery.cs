using EnergyHeatMap.Contracts.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Queries
{
    public class GetFilteredCryptoCoinStatesQuery : IRequest<IEnumerable<ICryptoCoinState>>
    {
        public GetFilteredCryptoCoinStatesQuery(string coinname, DateTime startdate, DateTime enddate)
        {
            Coinname = coinname;
            Startdate = startdate;
            Enddate = enddate;
        }

        public string Coinname { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
    }
}
