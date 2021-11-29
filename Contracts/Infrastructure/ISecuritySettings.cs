using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Contracts.Infrastructure
{
    public interface ISecuritySettings
    {
        string? Secret { get; set; }
        string? AdminUsername { get; set; }
        string? AdminPassword { get; set; }
    }
}
