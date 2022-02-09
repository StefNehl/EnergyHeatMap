using CsvHelper;
using EnergyHeatMap.Contracts.Entities;
using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Domain;
using EnergyHeatMap.Domain.Entities;
using EnergyHeatMap.Domain.Extensions;
using EnergyHeatMap.Domain.Models;
using EnergyHeatMap.Infrastructure.Helpers;
using EnergyHeatMap.Infrastructure.Queries.Domain;
using MediatR;
using Microsoft.Extensions.Options;
using System.Data;
using System.Diagnostics;
using System.Text.Json;

namespace EnergyHeatMap.Infrastructure.Services
{
    public class CryptoCoinStateService : ICryptoCoinStateService
    {
        private readonly IDataUnitService _dataUnitService;

        private List<ICryptoCoinStateEntity> _cryptoCoinStateEntities;
        private readonly string _cryptoDataPath;
        private const string BtcHistoricalDataFilename = @"Bitcoin Historical Data.csv";
        private const string BtcHashRateFilename = @"btc_hashrate.json";

        private const string EthHistoricalDataFilename = @"ethereum_2015-8-7_2021-11-29.csv";
        private const string EthHashRateFilename = @"eth_hashrate.json";

        public CryptoCoinStateService(IOptionsMonitor<DataPathSettings> optionsMonitor, IDataUnitService dataUnitService)
        {
            _dataUnitService = dataUnitService;
            try
            {
                var path = optionsMonitor.CurrentValue.DataPathCrypto;
                if(path == null)
                    throw new ArgumentNullException(nameof(_cryptoDataPath));

                _cryptoDataPath = DataPathGenerator.Create(path);
            }
            catch (Exception)
            {
                throw;
            }

            _cryptoCoinStateEntities = new List<ICryptoCoinStateEntity>();
            LoadBtcHistoricalData();
            LoadBtcHashrateData();

            LoadEthHistoricaldata();
            LoadEthHashrateData();

        }

        private void LoadBtcHistoricalData()
        {
            LoadCsvData(BtcHistoricalDataFilename, true);
        }

        private void LoadBtcHashrateData()
        {
            LoadJsonData(BtcHashRateFilename, CoinName.Btc);
        }


        private void LoadEthHistoricaldata()
        {
            LoadCsvData(EthHistoricalDataFilename, false);
        }

        private void LoadEthHashrateData()
        {
            LoadJsonData(EthHashRateFilename, CoinName.Eth);
        }


        private void LoadCsvData(string filename, bool isBtc)
        {
            var path = _cryptoDataPath + filename;

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

                    var coinEnum = CoinName.None;
                    if (isBtc)
                        coinEnum = CoinName.Btc;
                    else
                        coinEnum = CoinName.Eth;

                    //Take high value 
                    var valueString = csvReader.GetField(2);
                    var value = _dataUnitService.ConvertValue(double.Parse(valueString));

                    var btcDataItem = new CryptoCoinStateEntity(dateTime, coinEnum, value, _dataUnitService.UnitValue);
                    _cryptoCoinStateEntities.Add(btcDataItem);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool LoadJsonData(string filename, string coinname)
        {
            try
            {
                var jsonString = File.ReadAllText(_cryptoDataPath + filename);
                var hashRateData = JsonSerializer.Deserialize<List<object>>(jsonString);

                if (hashRateData == null)
                    throw new ArgumentNullException(nameof(_cryptoDataPath));

                foreach (var dataSet in hashRateData)
                {
                    var jsonElement = (JsonElement)dataSet;

                    var ticks = jsonElement[0].GetInt64();
                    var value = jsonElement[1].GetDouble();

                    var timeSpan = TimeSpan.FromMilliseconds(ticks);
                    var dateTime = new DateTime(1970, 1, 1) + timeSpan;
                    var hdSet = _cryptoCoinStateEntities
                        .FirstOrDefault(i => i.CoinName == coinname && 
                        i.DateTime.Ticks == dateTime.Ticks);

                    if (hdSet == null)
                        continue;

                    //10^18 to get the same value than the country depending hashrate values (EH/S)
                    hdSet.Hashrate = _dataUnitService.ConvertHashrate(value / Math.Pow(10,18));
                    hdSet.HashrateUnit = _dataUnitService.UnitHashrate;
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
            string[] coinnames, 
            DateTime startdate = default,
            DateTime enddate = default,
            CancellationToken ct = default)
        {
            await Task.Delay(5, ct);

            if (_cryptoCoinStateEntities.Count == 0)
                return new List<ICryptoCoinState>();

            //Start of crypto (not really correct) 
            if (startdate == default)
                startdate = new DateTime(2000, 1, 1);

            //stopped tracking data in Jan 2022
            if (enddate == default)
                enddate = DateTime.Now;

            var filterdList = _cryptoCoinStateEntities
                .Where(j =>
                    j.DateTime >= startdate &&
                    j.DateTime <= enddate);

            if (coinnames.Length != 0)
            {                
                filterdList = filterdList.Where(j => coinnames.Contains(j.CoinName));
            }

            return filterdList
                .Select(i => i.ToModel());
        }

        public async Task<IEnumerable<ICryptoStateData>> GetCryptoCoinDataFilteredByType(
            string[] coinNames,
            string[] types,
            DateTime startdate,
            DateTime enddate, 
            CancellationToken ct)
        {
            var fileredData = await GetCryptoCoinStateByFilter(coinNames, startdate, enddate, ct);
            var resultByType = new List<ICryptoStateData>();

            foreach (var coin in coinNames)
            {
                foreach (var typeString in types)
                {
                    if (!Enum.TryParse(typeString, out CryptoValueTypes type))
                        continue;

                    var values = Array.Empty<IDateTimeWithValue>();
                    var unit = string.Empty;

                    var filteredResult = fileredData.Where(i => i.CoinName == coin);

                    switch (type)
                    {
                        case CryptoValueTypes.Value:
                            values = filteredResult.Select(i =>
                            {
                                return new DateTimeWithValue(i.DateTime, i.Value);
                            }).ToArray();
                            unit = _dataUnitService.UnitValue;
                            break;
                        case CryptoValueTypes.Hashrate:
                            values = filteredResult.Select(i =>
                            {
                                return new DateTimeWithValue(i.DateTime, i.Hashrate);
                            }).ToArray();
                            unit = _dataUnitService.UnitHashrate;
                            break;
                        default:
                            continue;
                    }

                    var newData = new CryptoStateData(coin, typeString, unit, values);
                    resultByType.Add(newData);
                }
            }

            return resultByType;
        }

        public async Task<IEnumerable<string>> GetCryptoCoins(CancellationToken ct)
        {
            var groups = _cryptoCoinStateEntities.GroupBy(i => i.CoinName);
            await Task.Yield();

            return groups.Where(i => i.Key == CoinName.Btc).Select(i => i.Key);
        }

        public IEnumerable<ICryptoValueType> GetCryptoCoinValueTypes(CancellationToken ct)
        {
            return CryptoValueTypesExtensions.GetValues();
        }
    }
}
