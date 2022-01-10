namespace EnergyHeatMap.Contracts.Repositories
{
    public interface ICountryEnergyStateServices
    {
        public Task<IEnumerable<string>> GetCountries(CancellationToken ct);
    }
}
