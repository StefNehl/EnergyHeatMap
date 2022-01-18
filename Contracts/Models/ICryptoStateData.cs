namespace EnergyHeatMap.Contracts.Models
{
    public interface ICryptoStateData
    {
        string CoinName { get; set; }
        string Unit { get; set; }
        IDateTimeWithValue[]? Values { get; set; }
        string ValueType { get; set; }
    }
}