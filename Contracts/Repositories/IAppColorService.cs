using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Repositories
{
    public interface IAppColorService
    {
        Color MainColor { get; set; }
        Color[] SeriesColors { get; set; }
        int AlphaValueForOpacity { get; set; }    
    }
}
