using EnergyHeatMap.Contracts.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Queries.Chart
{
    public class GetFilteredCryptoCoinStatesByTypeQuery : IRequest<IEnumerable<ICryptoStateData>>
    {
        public GetFilteredCryptoCoinStatesByTypeQuery(string[] coinnames, string[] types, DateTime startdate, DateTime enddate)
        {
            Coinnames = coinnames;
            Startdate = startdate;
            Enddate = enddate;
            Types = types;
        }

        public string[] Coinnames { get; set; }
        public string[] Types { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
    }
}
