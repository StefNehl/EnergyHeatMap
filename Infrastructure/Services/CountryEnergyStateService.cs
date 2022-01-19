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
        private readonly string _countryEnergyStatesPath;
        private const string CountryEnergyStateFileName = "owid-energy-data.csv";

        private const string AllCountries = "World";

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

            _countryEnergyStates = new List<ICountryEnergyStateEntity>();
            LoadCsvData(CountryEnergyStateFileName);
        }

        private void LoadCsvData(string filename)
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

                    var dateString = csvReader.GetField(2)?.ToString();

                    if (string.IsNullOrWhiteSpace(dateString))
                        continue;

                    if (!int.TryParse(dateString, out var year))
                        continue;

                    if (year < 2000)
                        continue;

                    var dataItem = new CountryEnergyStateEntity()
                    {
                        DateTime = new DateTime(year, 1, 1)
                    };

                    dataItem.IsoCode = csvReader.GetField(0);
                    dataItem.Country = csvReader.GetField(1);

                    var electricGenerationString = csvReader.GetField(28);
                    if (decimal.TryParse(electricGenerationString, out decimal electricGeneration))
                        dataItem.Electricity_generation = electricGeneration;

                    var populationString = csvReader.GetField(98);
                    if(long.TryParse(populationString, out long population))
                        dataItem.Population = population;

                    var primaryEnergyConString = csvReader.GetField(99);
                    if (decimal.TryParse(primaryEnergyConString, out decimal primaryEnergyCon))
                        dataItem.Primary_energy_consuption = primaryEnergyCon;

                    _countryEnergyStates.Add(dataItem);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetCountries(CancellationToken ct)
        {
            var groups = _countryEnergyStates.GroupBy(x => x.Country)
                .Select(i => i.Key)
                .OrderBy(i => i)
                .ToList();
            await Task.Yield();

            groups.Remove(AllCountries);

            var countries = new List<string>();
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
                startdate = new DateTime(2000, 0, 0);

            //stopped tracking data in Nov 2021
            if (enddate == default)
                enddate = new DateTime(2022, 0, 0);



            var timeFilteredResult = _countryEnergyStates
                                .Where(j =>
                                    j.DateTime >= startdate &&
                                    j.DateTime <= enddate);

            var resultByType = new List<EnergyStateData>();

            foreach(var country in countries)
            {
                foreach(var typeString in types)
                {
                    if (!Enum.TryParse(typeString, out EnergyStateValueTypes type))
                        continue;

                    var newData = new EnergyStateData()
                    {
                        Country = country,
                        EnergyStateValueTypes = typeString,
                    };

                    var filteredData = timeFilteredResult.Where(i => i.Country == country);

                    switch (type)
                    {
                        //case EnergyStateValueTypes.Population:
                        //    newData.Values = filteredData.Select(i =>
                        //    {
                        //        return new DateTimeWithValue()
                        //        {
                        //            DateTime = i.DateTime,
                        //            Value = i.Population
                        //        };
                        //    }).ToArray();
                        //    break;
                        case EnergyStateValueTypes.ElectricityGeneration:
                            newData.Values = filteredData.Select(i =>
                            {
                                return new DateTimeWithValue()
                                {
                                    DateTime = i.DateTime,
                                    Value = (double)i.Electricity_generation
                                };
                            }).ToArray();
                            break;
                        case EnergyStateValueTypes.PrimaryEnergyConsumption:
                            newData.Values = filteredData.Select(i =>
                            {
                                return new DateTimeWithValue()
                                {
                                    DateTime = i.DateTime,
                                    Value = (double)i.Primary_energy_consuption
                                };
                            }).ToArray();
                            break;
                        default:
                            continue;
                    }
                    resultByType.Add(newData);
                }
            }

            return resultByType;
        }
    }
}
