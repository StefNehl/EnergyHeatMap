using CsvHelper;
using EnergyHeatMap.Contracts.Entities;
using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Domain.Entities;
using EnergyHeatMap.Domain.Extensions;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;

namespace EnergyHeatMap.Infrastructure.Services
{
    public class CryptoCoinStateService : ICryptoCoinStateService
    {
        private List<ICryptoCoinStateEntity> _cryptoCoinStateEntities;
        private readonly string CryptoDataPath;
        private const string BtcHistoricalDataFileName = @"bitcoin_2010-10-1_2021-11-29.csv";
        private const string BtcHashRate = @"btc_hashrate.json";

        public CryptoCoinStateService(IOptionsMonitor<DataPathSettings> optionsMonitor)
        {
            try
            {
                var path = optionsMonitor.CurrentValue.DataPathCrypto;
                if(path == null)
                    throw new ArgumentNullException(nameof(CryptoDataPath));

                CryptoDataPath = path;
            }
            catch (Exception)
            {
                throw;
            }

            _cryptoCoinStateEntities = new List<ICryptoCoinStateEntity>();
            LoadBtcHistoricalData();
            LoadBtcHashrateData();
        }

        private bool LoadBtcHistoricalData()
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

        private bool LoadBtcHashrateData()
        {
            try
            {
                var jsonString = File.ReadAllText(CryptoDataPath + BtcHashRate);
                var hashRateData = JsonSerializer.Deserialize<IList<HashRateDataSet>>(jsonString);

                if (hashRateData == null)
                    throw new ArgumentNullException(nameof(CryptoDataPath));

                foreach (var hashRate in hashRateData)
                {
                    var dateTime = new DateTime(hashRate.Ticks);
                    var hdSet = _cryptoCoinStateEntities
                        .FirstOrDefault(i => i.DateTime == dateTime);

                    if (hdSet != null)
                        hdSet.Hashrate = hashRate.Hashrate;
                }
            }
            catch 
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

        private class HashRateDataSet
        {
            public long Ticks { get; set; }
            public decimal Hashrate { get; set; }
        }
    }
}
