using EnergyHeatMap.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Domain.Models
{
    public class AuthenticateModel : IAuthenticateModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
