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
        private readonly IDataPreparationService _dataPreparationService;

        private List<ICountryEnergyStateEntity> _countryEnergyStates;
        private List<ICountryHashrateEntity> _countryHashrate;
        private readonly string _countryEnergyStatesPath;
        private const string CountryEnergyStateFileName = "energy-data.csv";
        private const string HashrateProductionFileName = "hashrate_production.csv";

        private const string AllCountries = "World";
        private const int MinYear = 2010;

        public CountryEnergyStateService(IOptionsMonitor<DataPathSettings> optionsMonitor, 
            IDataUnitService dataUnitService, 
            IDataPreparationService dataPreparationService)
        {
            _dataUnitService = dataUnitService;
            _dataPreparationService = dataPreparationService;

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
        }

        public async Task InitService(CancellationToken ct)
        {
            await LoadCsvDataAsync(HashrateProductionFileName, false, ct);
            LoadWorldHashrate();
            await LoadCsvDataAsync(CountryEnergyStateFileName, true, ct);
            await Task.Run(() => InterpolateEnergyData(), ct);
            CalculateWorldEnergyData();
        }

        private async Task LoadCsvDataAsync(string filename, bool isEnergyData, CancellationToken ct)
        {
            var path = _countryEnergyStatesPath + filename;

            try
            {
                using var csvReader = new CsvReader(
                    new StreamReader(File.OpenRead(path)), new System.Globalization.CultureInfo("us"));

                var stepOverFirstLine = true;
                int count = 0;
                while (await csvReader.ReadAsync())
                {
                    count++;
                    if (stepOverFirstLine)
                    {
                        stepOverFirstLine = false;
                        continue;
                    }

                    if (isEnergyData)
                        await LoadEnergyDataAsync(csvReader, count, ct);
                    else
                        await LoadHashrateDataAsync(csvReader, count, ct);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task LoadEnergyDataAsync(CsvReader csvReader, int rowCount, CancellationToken ct)
        {
            try
            {
                var dateString = csvReader.GetField(2)?.ToString();

                if (string.IsNullOrWhiteSpace(dateString))
                    return;

                if (!int.TryParse(dateString, out var year))
                    return;

                if (year < MinYear)
                    return;


                var date = new DateTime(year, 12, 1);
                var isoCode = csvReader.GetField(0);
                var country = csvReader.GetField(1);

                if ((await GetCountries(ct)).Contains(country) == false)
                    return;

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

        private async Task LoadHashrateDataAsync(CsvReader csvReader, int rowCount, CancellationToken ct)
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

        private void InterpolateEnergyData()
        {
            var countries = _countryEnergyStates.GroupBy(x => x.Country).Select(i => i.Key);

            var interpolatedData = new List<ICountryEnergyStateEntity>();

            //Only Interpolate Data after 2000 
            var statesForInterPolation = _countryEnergyStates.Where(i => i.DateTime.Year >= MinYear);
            foreach(var country in countries)
            {
                var energyStatesForThisCountry = statesForInterPolation.Where(i => i.Country == country);

                var values = energyStatesForThisCountry
                    .Where(c => c.PrimaryEnergyConsuption != 0)
                    .Select(i => i.PrimaryEnergyConsuption).ToArray();

                if (values.Length == 0)
                    continue;

                var points = new double[values.Length];
                for (int i = 0; i < points.Length; i++)
                    points[i] = (i + 1) * 12;

                //20 Year with 12 Months
                var dataPointsNeeded = 20 * 12;

                var finishedPoints = new double[dataPointsNeeded];
                var finishedValues = new double[dataPointsNeeded];
                var pointsToInterPolate = new double[finishedPoints.Length - points.Length];

                for(int i = 0; i < finishedPoints.Length; i++)
                {
                    finishedPoints[i] = i + 1;
                    if((i + 1) % 12 == 0)
                    {
                        var valueIndex = ((i + 1) / 12) - 1;
                        if(valueIndex < values.Length && values[valueIndex] != 0)
                            finishedValues[i] = values[valueIndex];
                    }
                }

                var arrayToInterpolate = new Tuple<double, double>[dataPointsNeeded];

                for(int i = 0; i < arrayToInterpolate.Length; i++)
                    arrayToInterpolate[i] = new Tuple<double, double>(finishedPoints[i], finishedValues[i]);

                var interPolatedValues = _dataPreparationService.Interpolate(
                    points, 
                    values,
                    arrayToInterpolate);
                //Replace value with interpolatedValues

                var baseDateTime = new DateTime(MinYear, 1, 1);
                var baseState = energyStatesForThisCountry.FirstOrDefault();
                if (baseState == null)
                    continue;

                foreach (var value in interPolatedValues)
                {
                    var valueDate = baseDateTime.AddMonths((int)value.Item1);
                    var state = energyStatesForThisCountry.FirstOrDefault(i => i.DateTime == valueDate);

                    if (state == null)
                    {
                        state = new CountryEnergyStateEntity(
                            baseState.IsoCode, 
                            baseState.Country, 
                            valueDate, 
                            0, 
                            value.Item2, 
                            baseState.PrimaryEnergyConsuptionUnit, 
                            0, 
                            baseState.PrimaryEnergyConsuptionUnit);
                        _countryEnergyStates.Add(state);
                    }

                    if (state.PrimaryEnergyConsuption == 0)
                        state.PrimaryEnergyConsuption = value.Item2;

                }

            }

            _countryEnergyStates = _countryEnergyStates.OrderBy(i => i.Country).ThenBy(j => j.DateTime).ToList();
        }

        private void CalculateWorldEnergyData()
        {
            var worldStates = _countryEnergyStates.Where(i => i.Country == AllCountries);

            foreach (var worldState in worldStates)
            {
                var countries = _countryEnergyStates.Where(i => i.DateTime == worldState.DateTime && i.Country != AllCountries);
                worldState.PrimaryEnergyConsuption = countries.Sum(i => i.PrimaryEnergyConsuption);
            }
        }

        public async Task<IEnumerable<string>> GetCountries(CancellationToken ct)
        {
            var groups = _countryHashrate.GroupBy(x => x.Country)
                .Select(i => i.Key)
                .OrderBy(i => i)
                .ToList();

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
                                energyPercentage = (energy.PrimaryEnergyConsuption / worldEnergy.PrimaryEnergyConsuption) * 100;
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
