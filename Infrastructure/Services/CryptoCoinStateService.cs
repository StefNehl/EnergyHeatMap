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
        private const string BtcHistoricalDataFilename = @"bitcoin_2010-10-1_2021-11-29.csv";
        private const string BtcHashRateFilename = @"btc_hashrate.json";
        private const string BtcDifficultyFilename = @"btc_difficulty.json";

        private const string EthHistoricalDataFilename = @"ethereum_2015-8-7_2021-11-29.csv";
        private const string EthHashRateFilename = @"eth_hashrate.json";
        private const string EthDifficultyFilename = @"eth_difficulty.json";

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
            LoadBtcDifficulty();

            LoadEthHistoricaldata();
            LoadEthHashrateData();
            LoadEthDifficulty();
        }

        private bool LoadBtcHistoricalData()
        {
            return LoadCsvData(BtcHistoricalDataFilename, true);
        }

        private bool LoadBtcHashrateData()
        {
            return LoadJsonData(BtcHashRateFilename, true);
        }

        private bool LoadBtcDifficulty()
        {
            return LoadJsonData(BtcDifficultyFilename, false);
        }


        private bool LoadEthHistoricaldata()
        {
            return LoadCsvData(EthHistoricalDataFilename, false);
        }

        private bool LoadEthHashrateData()
        {
            return LoadJsonData(EthHashRateFilename, true);
        }

        private bool LoadEthDifficulty()
        {
            return LoadJsonData(EthDifficultyFilename, false);
        }


        private bool LoadCsvData(string filename, bool isBtc)
        {
            var path = CryptoDataPath + filename;
            try
            {
                using var csvReader = new CsvReader(
                    new StreamReader(File.OpenRead(path)), new System.Globalization.CultureInfo("us"));

                while (csvReader.Read())
                {
                    var dateString = csvReader.GetField(0)?.ToString();

                    if (string.IsNullOrWhiteSpace(dateString))
                        continue;

                    if (!DateTime.TryParse(dateString, out DateTime dateTime))
                        continue;

                    var btcDataItem = new CryptoCoinStateEntity()
                    {
                        DateTime = dateTime
                    };

                    if (isBtc)
                        btcDataItem.CoinName = Contracts.Enums.CoinName.Btc;
                    else
                        btcDataItem.CoinName = Contracts.Enums.CoinName.Eth;

                    //Take high value 
                    var valueString = csvReader.GetField(2);
                    if (decimal.TryParse(valueString, out decimal value))
                        btcDataItem.Value = value;

                    _cryptoCoinStateEntities.Add(btcDataItem);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        private bool LoadJsonData(string filename, bool isHashRate)
        {
            try
            {
                var jsonString = File.ReadAllText(CryptoDataPath + filename);
                var hashRateData = JsonSerializer.Deserialize<List<object>>(jsonString);

                if (hashRateData == null)
                    throw new ArgumentNullException(nameof(CryptoDataPath));

                foreach (var dataSet in hashRateData)
                {
                    var jsonElement = (JsonElement)dataSet;

                    var ticks = jsonElement[0].GetInt64();
                    var value = jsonElement[1].GetDecimal();

                    var timeSpan = TimeSpan.FromMilliseconds(ticks);
                    var dateTime = new DateTime(1970, 1, 1) + timeSpan;
                    var hdSet = _cryptoCoinStateEntities
                        .FirstOrDefault(i => i.DateTime.Ticks == dateTime.Ticks);

                    if (hdSet == null)
                        continue;

                    if (isHashRate)
                        hdSet.Hashrate = value;
                    else
                        hdSet.Difficulty = value;
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

        public async Task<IEnumerable<ICryptoCoinState>> GetCryptoCoinStateByFilter(
            string coinname = Contracts.Enums.CoinName.None, 
            DateTime startdate = default,
            DateTime enddate = default,
            CancellationToken ct = default)
        {
            await Task.Delay(5, ct);

            if (_cryptoCoinStateEntities.Count == 0)
                return new List<ICryptoCoinState>();

            //Start of crypto (not really correct) 
            if (startdate == default)
                startdate = new DateTime(2000, 0, 0);

            //stopped tracking data in Nov 2021
            if (enddate == default)
                enddate = new DateTime(2022, 0, 0);

            var filterdList = _cryptoCoinStateEntities
                .Where(j =>
                    j.DateTime >= startdate &&
                    j.DateTime <= enddate);

            if (coinname != Contracts.Enums.CoinName.None)
                filterdList = filterdList.Where(j => j.CoinName == coinname);

            return filterdList
                .Where(j => 
                    j.DateTime >= startdate &&
                    j.DateTime <= enddate)
                .Select(i => i.ToModel());
        }

        public async Task<IEnumerable<string>> GetCryptoCoins(CancellationToken ct)
        {
            var groups = _cryptoCoinStateEntities.GroupBy(i => i.CoinName);
            await Task.Yield();

            return groups.Select(i => i.Key);
        }

        private class HashRateDataSet
        {
            public long Ticks { get; set; }
            public decimal Hashrate { get; set; }
        }
    }
}
