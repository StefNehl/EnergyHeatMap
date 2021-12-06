using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Models
{
    public interface IAuthenticateModel
    {
        string? Username { get; set; }
        string? Password { get; set; }
    }
}
