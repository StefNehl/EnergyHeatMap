using CsvHelper;
using EnergyHeatMap.Contracts.Entities;
using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Domain.Entities;
using EnergyHeatMap.Domain.Enums;
using EnergyHeatMap.Domain.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Services
{
    public class CountryEnergyStateService : ICountryEnergyStateServices
    {
        private readonly List<ICountryEnergyStateEntity> _countryEnergyStates;
        private readonly List<ICountryHashrateEntity> _countryHashrate;
        private readonly string _countryEnergyStatesPath;
        private const string CountryEnergyStateFileName = "owid-energy-data.csv";
        private const string HashrateProductionFileName = "hashrate_production.csv";

        private readonly double hashrateConvRate;
        private const string UnitHashrate = "EH/s";

        private const string AllCountries = "World";
        private readonly double energyConvRate;
        private const string UnitEnergy = "TWh";

        public CountryEnergyStateService(IOptionsMonitor<DataPathSettings> optionsMonitor)
        {
            try
            {
                var path = optionsMonitor.CurrentValue.DataPathEnergy;
                if (path == null)
                    throw new ArgumentNullException(nameof(_countryEnergyStatesPath));

                _countryEnergyStatesPath = path;
            }
            catch (Exception)
            {
                throw;
            }

            hashrateConvRate = 1;//Math.Pow(10, 3);
            energyConvRate = 1;// Math.Pow(10, 3);

            _countryEnergyStates = new List<ICountryEnergyStateEntity>();
            _countryHashrate = new List<ICountryHashrateEntity>();
            LoadCsvData(CountryEnergyStateFileName, true);
            LoadCsvData(HashrateProductionFileName, false);
            LoadWorldHashrate();
        }

        private void LoadCsvData(string filename, bool isEnergyData)
        {
            var path = _countryEnergyStatesPath + filename;

            try
            {
                using var csvReader = new CsvReader(
                    new StreamReader(File.OpenRead(path)), new System.Globalization.CultureInfo("us"));

                var stepOverFirstLine = true;
                while (csvReader.Read())
                {
                    if (stepOverFirstLine)
                    {
                        stepOverFirstLine = false;
                        continue;
                    }

                    if (isEnergyData)
                        LoadEnergyData(csvReader);
                    else
                        LoadHashrateData(csvReader);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadEnergyData(CsvReader csvReader)
        {
            var dateString = csvReader.GetField(2)?.ToString();

            if (string.IsNullOrWhiteSpace(dateString))
                return;

            if (!int.TryParse(dateString, out var year))
                return;

            if (year < 2000)
                return;

            var dataItem = new CountryEnergyStateEntity()
            {
                DateTime = new DateTime(year, 12, 31)
            };

            dataItem.IsoCode = csvReader.GetField(0);
            dataItem.Country = csvReader.GetField(1);

            var electricGenerationString = csvReader.GetField(29);
            if (decimal.TryParse(electricGenerationString, out decimal electricGeneration))
                dataItem.Electricity_generation = electricGeneration * (decimal)energyConvRate;

            var populationString = csvReader.GetField(99);
            if (long.TryParse(populationString, out long population))
                dataItem.Population = population;

            var primaryEnergyConString = csvReader.GetField(100);
            if (decimal.TryParse(primaryEnergyConString, out decimal primaryEnergyCon))
                dataItem.Primary_energy_consuption = primaryEnergyCon * (decimal)energyConvRate;

            _countryEnergyStates.Add(dataItem);
        }

        private void LoadHashrateData(CsvReader csvReader)
        {
            var newData = new CountryHashrateEntity()
            {
                Country = csvReader.GetField(1)
            };

            var dateString = csvReader.GetField(0)?.ToString();

            if (string.IsNullOrWhiteSpace(dateString))
                return;

            if (!DateTime.TryParse(dateString, out DateTime dateTime))
                return;

            newData.DateTime = dateTime;

            var percString = csvReader.GetField(2)?.ToString();
            if (string.IsNullOrWhiteSpace(percString))
                return;

            percString = percString.Substring(0, percString.Length - 1);

            if (!double.TryParse(percString, out double percentage))
                return;

            newData.MonthlyHashratePercentage = percentage;

            var absString = csvReader.GetField(3)?.ToString();
            if (string.IsNullOrWhiteSpace(absString))
                return;

            if (!double.TryParse(absString, out double abs))
                return;

            newData.MonthlyHashrateAbsolut = abs;

            _countryHashrate.Add(newData);
        }

        private void LoadWorldHashrate()
        {
            var dateGroups = _countryHashrate.GroupBy(x => x.DateTime);

            foreach (var group in dateGroups)
            {
                var dataItem = new CountryHashrateEntity()
                {
                    Country = AllCountries,
                    DateTime = group.Key,
                    MonthlyHashrateAbsolut = group.Sum(i => i.MonthlyHashrateAbsolut),
                    MonthlyHashratePercentage = 100
                };
                _countryHashrate.Add(dataItem);
            }
        }

        public async Task<IEnumerable<string>> GetCountries(CancellationToken ct)
        {
            var groups = _countryHashrate.GroupBy(x => x.Country)
                .Select(i => i.Key)
                .OrderBy(i => i)
                .ToList();
            await Task.Yield();

            var countries = new List<string>();

            groups.Remove(AllCountries);
            countries.Add(AllCountries);
            countries.AddRange(groups);
            return countries;
        }

        public async Task<IEnumerable<IEnergyStateData>> GetEnergyStateDataByType(string[] countries,
            string[] types,
            DateTime startdate = default,
            DateTime enddate = default,
            CancellationToken ct = default)
        {
            //Start of crypto (not really correct) 
            if (startdate == default)
                startdate = new DateTime(2000, 1, 1);

            //stopped tracking data in Jan 2022
            if (enddate == default)
                enddate = DateTime.Now;



            var timeFilteredEnergyDate = _countryEnergyStates
                .Where(j => j.DateTime >= startdate &&
                            j.DateTime <= enddate);

            var timeFilteredHashrateDate = _countryHashrate
                .Where(j => j.DateTime >= startdate &&
                            j.DateTime <= enddate);

            var resultByType = new List<EnergyStateData>();

            foreach (var country in countries)
            {
                foreach (var typeString in types)
                {
                    if (!Enum.TryParse(typeString, out EnergyStateValueTypes type))
                        continue;

                    var filteredEnergyData = timeFilteredEnergyDate.Where(i => i.Country == country);
                    var filteredHashrateData = timeFilteredHashrateDate.Where(i => i.Country == country);
                    var unit = "";
                    var values = Array.Empty<IDateTimeWithValue>();

                    switch (type)
                    {
                        case EnergyStateValueTypes.Population:
                            values = filteredEnergyData.Select(i =>
                            {
                                return new DateTimeWithValue()
                                {
                                    DateTime = i.DateTime,
                                    Value = (double)i.Primary_energy_consuption
                                };
                            }).ToArray();
                            unit = "#";
                            break;
                        case EnergyStateValueTypes.ElectricityGeneration:
                            values = filteredEnergyData.Select(i =>
                            {
                                return new DateTimeWithValue()
                                {
                                    DateTime = i.DateTime,
                                    Value = (double)i.Electricity_generation
                                };
                            }).ToArray();
                            unit = UnitEnergy;
                            break;
                        case EnergyStateValueTypes.PrimaryEnergyConsumption:
                            values = filteredEnergyData.Select(i =>
                            {
                                return new DateTimeWithValue()
                                {
                                    DateTime = i.DateTime,
                                    Value = (double)i.Primary_energy_consuption
                                };
                            }).ToArray();
                            unit = UnitEnergy;
                            break;
                        case EnergyStateValueTypes.HashrateProductionInPercentage:
                            values = filteredHashrateData.Select(i =>
                            {
                                return new DateTimeWithValue()
                                {
                                    DateTime = i.DateTime,
                                    Value = (double)i.MonthlyHashratePercentage
                                };
                            }).ToArray();
                            unit = "%";
                            break;
                        case EnergyStateValueTypes.HashrateProductionInAbs:
                            values = filteredHashrateData.Select(i =>
                            {
                                return new DateTimeWithValue()
                                {
                                    DateTime = i.DateTime,
                                    Value = (double)i.MonthlyHashrateAbsolut * hashrateConvRate
                                };
                            }).ToArray();
                            unit = UnitHashrate;
                            break;
                        default:
                            continue;
                    }

                    var prettyTypeString = EnergyStateValueTypesExtensions.GetPrettyString(type);
                    var newData = new EnergyStateData("", country, prettyTypeString, unit, values.ToArray());
                    resultByType.Add(newData);
                }
            }

            return resultByType;
        }

        public async Task<IEnumerable<ICountryDataModel>> GetCountriesData()
        {
            var hashrateData = _countryHashrate.Where(c => c.Country != AllCountries).ToList();
            var countries = hashrateData.Select(c => c.Country).ToList();
            var energyData = _countryEnergyStates.Where(j => countries.Contains(j.Country)).ToList();
            var worldEnergyStates = _countryEnergyStates.Where(i => i.Country == AllCountries);

            var result = new List<ICountryDataModel>();

            foreach (var country in countries)
            {
                var hashrates = hashrateData.Where(c => c.Country == country);

                if (hashrates == null)
                    continue;

                foreach (var hashrate in hashrates)
                {
                    var energy = energyData.FirstOrDefault(e => e.Country == country && e.DateTime.Year == hashrate.DateTime.Year);
                    if (energy == null)
                        continue;

                    var worldEnergy = worldEnergyStates.FirstOrDefault(e => e.DateTime == energy.DateTime);
                    if (worldEnergy == null)
                        continue;

                    var energyPercentage = 0.0m;
                    if (worldEnergy.Primary_energy_consuption != 0)
                        energyPercentage = energy.Primary_energy_consuption / worldEnergy.Primary_energy_consuption;

                    var newCountryData = new CountryDataModel(country, hashrate.DateTime,
                        hashrate.MonthlyHashrateAbsolut,
                        UnitHashrate,
                        hashrate.MonthlyHashratePercentage,
                        (double)energy.Primary_energy_consuption,
                        UnitEnergy,
                        (double)energyPercentage);

                    result.Add(newCountryData);
                }
            }

            return result;
        }

        public async Task<Dictionary<string, IEnumerable<ICountryDataModel>>> GetCountriesDataGroupedByCountry()
        {
            var countriesData = await GetCountriesData();

            var groups = countriesData.GroupBy(i => i.CountryName);
            var result = new Dictionary<string, IEnumerable<ICountryDataModel>>();  

            foreach (var group in groups)
            {
                var countryGroupKey = group.Key;
                var values = group;
                result.Add(countryGroupKey, group.ToList());
            }

            return result;
        }

        public async Task<Dictionary<DateTime, IEnumerable<ICountryDataModel>>> GetCountriesDataGroupedByDateTime()
        {
            var countriesData = await GetCountriesData();

            var groups = countriesData.GroupBy(i => i.DateTime);
            var result = new Dictionary<DateTime, IEnumerable<ICountryDataModel>>();

            foreach (var group in groups)
            {
                var countryGroupKey = group.Key;
                var values = group;
                result.Add(countryGroupKey, group.ToList());
            }

            return result;
        }

    }
}
