using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Models
{
    public interface ICountryEnergyStateModel
    {
        string IsoCode { get; set; }
        string Country { get; set; }
        DateTime Year { get; set; }
        decimal Population { get; set; }
        decimal Primary_energy_consuption { get; set; }
        decimal Electricity_generation { get; set; }
    }
}
