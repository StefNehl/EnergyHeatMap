using CsvHelper;
using EnergyHeatMap.Contracts.Entities;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Domain.Entities;
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

        private const string AllCountries = "All";

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


                        

                    var dataItem = new CountryEnergyStateEntity()
                    {
                        DateTime = DateTime.Now
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
            var groups = _countryEnergyStates.GroupBy(x => x.Country);
            await Task.Yield();

            var countries = new List<string>();
            countries.Add(AllCountries);

            countries.AddRange(groups.Select(i => i.Key).OrderBy(i => i).ToList());
            return countries;
        }
    }
}
