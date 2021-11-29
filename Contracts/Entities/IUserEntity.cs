using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Entities
{
    public interface IUserEntity
    {
        DateTime CreatedOn { get; set; }
        Guid Id { get; set; }
        string Password { get; set; }
        string Username { get; set; }
        string Role { get; set; }
    }
}
