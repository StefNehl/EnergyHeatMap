using CsvHelper;
using EnergyHeatMap.Contracts.Entities;
using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Domain.Entities;
using EnergyHeatMap.Domain.Extensions;
using System.Data;

namespace EnergyHeatMap.Infrastructure.Services
{
    public class CryptoCoinStateService : ICryptoCoinStateService
    {
        private List<ICryptoCoinStateEntity> _cryptoCoinStateEntities;
        private const string CryptoDataPath = @"C:\projects\EnergyHeatMap\Data\crypto\";
        private const string BtcHistoricalDataFileName = @"bitcoin_2010-10-1_2021-11-29.csv";

        public CryptoCoinStateService()
        {
            _cryptoCoinStateEntities = new List<ICryptoCoinStateEntity>();
            LoadBtcData();
        }

        private bool LoadBtcData()
        {
            var path = CryptoDataPath + BtcHistoricalDataFileName;
            try
            {
                using var csvReader = new CsvReader(
                    new StreamReader(File.OpenRead(path)), new System.Globalization.CultureInfo("us"));

                while(csvReader.Read())
                {

                    var dateString = csvReader.GetField(0)?.ToString();

                    if (string.IsNullOrWhiteSpace(dateString))
                        continue;

                    if (!DateTime.TryParse(dateString, out DateTime dateTime))
                        continue;

                    _cryptoCoinStateEntities.Add(new CryptoCoinStateEntity()
                    {
                        CoinName = Contracts.Enums.CoinName.Btc,
                        DateTime = dateTime
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public async Task<IEnumerable<ICryptoCoinState>> GetAllAsync(CancellationToken ct)
        {
            await Task.Delay(5, ct);

            if (_cryptoCoinStateEntities.Count == 0)
                return new List<ICryptoCoinState>();
            return _cryptoCoinStateEntities.Select(i => i.ToModel());
        }
    }
}
