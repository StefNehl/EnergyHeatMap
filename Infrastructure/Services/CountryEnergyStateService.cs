using CsvHelper;
using EnergyHeatMap.Contracts.Entities;
using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Domain.Entities;
using EnergyHeatMap.Domain.Enums;
using EnergyHeatMap.Domain.Models;
using EnergyHeatMap.Infrastructure.Helpers;
using EnergyHeatMap.Infrastructure.Queries.Domain;
using MediatR;
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
        private readonly IDataUnitService _dataUnitService;

        private readonly List<ICountryEnergyStateEntity> _countryEnergyStates;
        private readonly List<ICountryHashrateEntity> _countryHashrate;
        private readonly string _countryEnergyStatesPath;
        private const string CountryEnergyStateFileName = "owid-energy-data.csv";
        private const string HashrateProductionFileName = "hashrate_production.csv";

        private const string AllCountries = "World";

        public CountryEnergyStateService(IOptionsMonitor<DataPathSettings> optionsMonitor, IDataUnitService dataUnitService)
        {
            _dataUnitService = dataUnitService;

            try
            {
                var path = optionsMonitor.CurrentValue.DataPathEnergy;
                if (path == null)
                    throw new ArgumentNullException(nameof(_countryEnergyStatesPath));

                _countryEnergyStatesPath = DataPathGenerator.Create(path);
            }
            catch (Exception)
            {
                throw;
            }

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
                int count = 0;
                while (csvReader.Read())
                {
                    count++;
                    if (stepOverFirstLine)
                    {
                        stepOverFirstLine = false;
                        continue;
                    }

                    if (isEnergyData)
                        LoadEnergyData(csvReader, count);
                    else
                        LoadHashrateData(csvReader, count);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadEnergyData(CsvReader csvReader, int rowCount)
        {
            try
            {
                var dateString = csvReader.GetField(2)?.ToString();

                if (string.IsNullOrWhiteSpace(dateString))
                    return;

                if (!int.TryParse(dateString, out var year))
                    return;

                if (year < 2000)
                    return;


                var date = new DateTime(year, 12, 31);
                var isoCode = csvReader.GetField(0);
                var country = csvReader.GetField(1);

                var populationString = csvReader.GetField(99);
                _ = double.TryParse(populationString, out double population);

                var primaryEnergyConString = csvReader.GetField(100);
                _ = double.TryParse(primaryEnergyConString, out double primaryEnergyCon);
                primaryEnergyCon = _dataUnitService.ConvertEnergy(primaryEnergyCon);

                var primaryEnergyConUnit = _dataUnitService.UnitEnergy;

                var electricGenerationString = csvReader.GetField(39);
                _ = double.TryParse(electricGenerationString, out double electricGeneration);
                electricGeneration = _dataUnitService.ConvertEnergy(electricGeneration);

                var electricityGenerationUnit = _dataUnitService.UnitEnergy;

                var dataItem = new CountryEnergyStateEntity(isoCode, country, date, population,
                    primaryEnergyCon, primaryEnergyConUnit,
                    electricGeneration, electricityGenerationUnit);
                _countryEnergyStates.Add(dataItem);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading energy in line: {rowCount}", ex);
            }
            
        }

        private void LoadHashrateData(CsvReader csvReader, int rowCount)
        {
            try
            {
                var country = csvReader.GetField(1);
                var dateString = csvReader.GetField(0)?.ToString();

                if (string.IsNullOrWhiteSpace(dateString))
                    return;

                var dateTime = DateTime.Parse(dateString);

                var percString = csvReader.GetField(2)?.ToString();
                if (string.IsNullOrWhiteSpace(percString))
                    return;
                percString = percString.Substring(0, percString.Length - 1);
                var percentage = double.Parse(percString);

                var absString = csvReader.GetField(3)?.ToString();
                if (string.IsNullOrWhiteSpace(absString))
                    return;
                var abs = _dataUnitService.ConvertHashrate(double.Parse(absString));

                var newData = new CountryHashrateEntity(dateTime, country, percentage, abs, _dataUnitService.UnitHashrate);
                _countryHashrate.Add(newData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading hashrate in line: {rowCount} and column: {csvReader.ColumnCount}", ex);
            }
        }

        private void LoadWorldHashrate()
        {
            var dateGroups = _countryHashrate.GroupBy(x => x.DateTime);

            foreach (var group in dateGroups)
            {
                var dataItem = new CountryHashrateEntity(group.Key, AllCountries, 100, group.Sum(i => i.MonthlyHashrateAbsolut), _dataUnitService.UnitHashrate);
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
                                return new DateTimeWithValue(i.DateTime, i.Population);
                            }).ToArray();
                            unit = "#";
                            break;
                        case EnergyStateValueTypes.ElectricityGeneration:
                            values = filteredEnergyData.Select(i =>
                            {
                                return new DateTimeWithValue(i.DateTime, i.ElectricityGeneration);
                            }).ToArray();
                            unit = _dataUnitService.UnitEnergy;
                            break;
                        case EnergyStateValueTypes.PrimaryEnergyConsumption:
                            values = filteredEnergyData.Select(i =>
                            {
                                return new DateTimeWithValue(i.DateTime, i.PrimaryEnergyConsuption);
                            }).ToArray();
                            unit = _dataUnitService.UnitEnergy;
                            break;
                        case EnergyStateValueTypes.HashrateProductionInPercentage:
                            values = filteredHashrateData.Select(i =>
                            {
                                return new DateTimeWithValue(i.DateTime, i.MonthlyHashratePercentage);
                            }).ToArray();
                            unit = "%";
                            break;
                        case EnergyStateValueTypes.HashrateProductionInAbs:
                            values = filteredHashrateData.Select(i =>
                            {
                                return new DateTimeWithValue(i.DateTime, i.MonthlyHashrateAbsolut);
                            }).ToArray();
                            unit = _dataUnitService.UnitHashrate;
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

        public async Task<IEnumerable<ICountryDataModel>> GetCountriesData(CancellationToken ct = default)
        {
            var hashrateData = _countryHashrate.Where(c => c.Country != AllCountries).ToList();
            var energyData = _countryEnergyStates.Where(j => j.Country != AllCountries).ToList();

            var timeSteps = hashrateData.Select(c => c.DateTime).Distinct().ToList();
            var countries = energyData.Select(c => c.Country).Distinct().ToList();
            var worldEnergyStates = _countryEnergyStates.Where(i => i.Country == AllCountries);

            var result = new List<ICountryDataModel>();

            var countryNameHelper = new CountryNameHelper();

            foreach (var country in countries)
            {
                foreach (var timeStep in timeSteps)
                {
                    var energyPercentage = 0.0;
                    var primaryEnergyConsuption = 0.0;

                    var energy = energyData.FirstOrDefault(e => e.Country == country && e.DateTime.Year == timeStep.Year);
                    if (energy != null)
                    {
                        primaryEnergyConsuption = energy.PrimaryEnergyConsuption;
                        var worldEnergy = worldEnergyStates.FirstOrDefault(e => e.DateTime == energy.DateTime);
                        if (worldEnergy != null)
                        {
                            if (worldEnergy.PrimaryEnergyConsuption != 0)
                                energyPercentage = energy.PrimaryEnergyConsuption / worldEnergy.PrimaryEnergyConsuption;
                        }
                    }

                    var hashratePerc = 0.0;
                    var hashrateAbs = 0.0;

                    var hashRate = hashrateData.FirstOrDefault(e => e.DateTime == timeStep && e.Country == country);
                    if(hashRate != null)
                    {
                        hashratePerc = hashRate.MonthlyHashratePercentage;
                        hashrateAbs = hashRate.MonthlyHashrateAbsolut;
                    }

                    var newCountryData = new CountryDataModel(country, countryNameHelper.GetShortName(country), timeStep,
                        hashrateAbs,
                        _dataUnitService.UnitHashrate,
                        hashratePerc,
                        (double)primaryEnergyConsuption,
                        _dataUnitService.UnitEnergy,
                        (double)energyPercentage);

                    result.Add(newCountryData);
                }
            }

            return result;
        }

        public async Task<Dictionary<string, IEnumerable<ICountryDataModel>>> GetCountriesDataGroupedByCountry(CancellationToken ct = default)
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

        public async Task<IEnumerable<IEnergyStateValueType>> GetEnergyStateValueTypes(CancellationToken ct = default)
        {
            return EnergyStateValueTypesExtensions.GetValues();
        }


    }
}
