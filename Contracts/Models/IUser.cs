using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Models
{
    public interface IUser
    {
        DateTime CreatedOn { get; set; }
        Guid Id { get; set; }
        string Token { get; set; }
        string Username { get; set; }
        string Role { get; set; }
    }
}
