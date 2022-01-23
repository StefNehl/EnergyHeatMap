namespace EnergyHeatMap.Contracts.Models
{
    public interface ICryptoStateData
    {
        string CoinName { get; }
        string Unit { get; }
        IDateTimeWithValue[]? Values { get; }
        string ValueType { get; }
        string Longname { get; }
    }
}