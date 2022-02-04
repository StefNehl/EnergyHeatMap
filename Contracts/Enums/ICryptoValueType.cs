using EnergyHeatMap.Contracts.Enums;

namespace EnergyHeatMap.Contracts.Enums
{
    public interface ICryptoValueType
    {
        string Name { get; }
        CryptoValueTypes Type { get; }
    }
}
