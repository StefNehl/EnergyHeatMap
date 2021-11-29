using EnergyHeatMap.Contracts.Infrastructure;
using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Infrastructure
{
    public class SecuritySettings : ISecuritySettings
    {
        public string? Secret { get; set; }
        public string? AdminUsername { get; set; }
        public string? AdminPassword { get; set; }
    }
}
