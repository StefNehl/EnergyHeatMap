using EnergyHeatMap.Contracts.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Queries.Chart
{
    public class GetFilteredEnergyStateDataQuery : IRequest<IEnumerable<IEnergyStateData>>
    {
        public GetFilteredEnergyStateDataQuery(string[] countries, string[] types, DateTime startdate, DateTime enddate)
        {
            Countries = countries;
            Types = types;
            Startdate = startdate;  
            Enddate = enddate;
        }

        public string[] Countries { get; set; }
        public string[] Types { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
    }
}
