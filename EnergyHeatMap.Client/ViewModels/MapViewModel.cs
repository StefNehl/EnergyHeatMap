using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Domain.Enums;
using EnergyHeatMap.Infrastructure.Queries;
using EnergyHeatMap.Infrastructure.Queries.HeatMap;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyHeatMap.Client.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private bool _isBusy;
        private HeatLandSeries[] _series;
        private int _selectedDataIndex;
        private DateTime _selectedDate;
        private int _selectionRangeMaxValue;
        private double _maxDataValue;
        private IEnumerable<Tuple<string, double>> _selectedData;
        private IEnumerable<IHeatMapValueType> _valueTypes;
        private IHeatMapValueType? _selectedHeatMapValueType;

        public MapViewModel()
        {
            _mediator = App.IoC.Services.GetService<IMediator>();
        }

        public async Task LoadAndSetMapData()
        {
            IsBusy = true;

            await LoadMapData();

            if(MapData.Keys.Count > 0)
                SelectionRangeMaxValue = MapData.Keys.Count - 1;
            SelectedDataIndex = SelectionRangeMaxValue;

            IsBusy = false;
        }

        private async Task LoadMapData()
        {
            var query = new GetAllCountriesDataGroupedByDateQuery();
            MapData = await _mediator.Send(query);
        }

        public async Task LoadHeatMapValueTypes()
        {
            var query = new GetHeatMapValueTypesQuery();
            ValueTypes = await _mediator.Send(query);
            _selectedHeatMapValueType = ValueTypes.FirstOrDefault(i => i.Type == HeatMapValueTypes.HashratePerc);
            this.RaisePropertyChanged(nameof(SelectedHeatMapValueType));
        }

        public async Task SetValuesForSelectedIndex()
        {
            var dateTime = MapData.Keys.ToArray()[SelectedDataIndex];
            var data = MapData[dateTime];

            if (data == null)
                return;

            if (Series == null)
            {
                var lands = new List<HeatLand>();
                foreach (var item in data)
                {
                    if (string.IsNullOrWhiteSpace(item.CountryCode))
                        continue;

                    var newLandData = new HeatLand()
                    {
                        Name = item.CountryCode,
                        Value = GetValueForHeatMapSettings(item)
                    };

                    lands.Add(newLandData);
                }

                Series = new HeatLandSeries[]
                {
                    new()
                    {
                        Lands = lands,
                    }
                };
            }
            else
            {
                await Task.Delay(100);

                foreach (var shape in Series[0].Lands)
                {
                    var dataItem = data.FirstOrDefault(i => i.CountryCode == shape.Name);
                    if (dataItem == null)
                        continue;

                    shape.Value = GetValueForHeatMapSettings(dataItem);
                }
            }

            MaxDataValue = data.Max(i => GetValueForHeatMapSettings(i));
            var dataSet = data.Select(i => new Tuple<string, double>(i.CountryName, GetValueForHeatMapSettings(i)));
            var oderedDataSet = dataSet.OrderByDescending(i => i.Item2).Take(10);
            SelectedData = oderedDataSet;
        }

        private double GetValueForHeatMapSettings(ICountryDataModel countryDataModel)
        {
            if (SelectedHeatMapValueType == null)
                return 0;

            switch (SelectedHeatMapValueType.Type)
            {
                case HeatMapValueTypes.HashratePerc:
                    return countryDataModel.HashratePerc;
                case HeatMapValueTypes.EnergyConsuptionPerc:
                    return countryDataModel.EnergyConsumptionPerc;
                default:
                    return 0;
            }
        }

        public HeatLandSeries[] Series 
        { 
            get => _series; 
            set => this.RaiseAndSetIfChanged(ref _series, value); 
        }

        public IDictionary<DateTime, IEnumerable<ICountryDataModel>> MapData {get;set; }

        public int SelectionRangeMaxValue
        {
            get => _selectionRangeMaxValue;
            set => this.RaiseAndSetIfChanged(ref _selectionRangeMaxValue, value);
        }

        public int SelectedDataIndex
        { 
            get => _selectedDataIndex;
            set
            {
                _selectedDataIndex = value;

                if(MapData.Count != 0)
                    SelectedDate = MapData.Keys.ToArray()[value];

                SetValuesForSelectedIndex();
                this.RaisePropertyChanged(nameof(SelectedDataIndex));
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate == value)
                    return;

                this.RaiseAndSetIfChanged(ref _selectedDate, value);
            }
        }

        public IEnumerable<Tuple<string, double>> SelectedData
        {
            get => _selectedData;
            set => this.RaiseAndSetIfChanged(ref _selectedData, value);
        }

        public bool IsBusy 
        { 
            get => _isBusy;
            set => this.RaiseAndSetIfChanged(ref _isBusy, value);
        }

        public double MaxDataValue
        {
            get => _maxDataValue;
            set => this.RaiseAndSetIfChanged(ref _maxDataValue, value);
        }

        public IEnumerable<IHeatMapValueType> ValueTypes
        {
            get => _valueTypes; 
            set => this.RaiseAndSetIfChanged(ref _valueTypes, value);
        }

        public IHeatMapValueType? SelectedHeatMapValueType
        {
            get => _selectedHeatMapValueType;
            set
            {
                if (value == _selectedHeatMapValueType)
                    return;

                SetValuesForSelectedIndex();
                this.RaiseAndSetIfChanged(ref _selectedHeatMapValueType, value);
            }
        }
    }
}
