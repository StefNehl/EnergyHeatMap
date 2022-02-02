using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Infrastructure.Queries;
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

        public MapViewModel()
        {
            _mediator = App.IoC.Services.GetService<IMediator>();
        }

        public async Task LoadAndSetMapData()
        {
            IsBusy = true;

            await Task.Delay(1000);
            await LoadMapData();

            SelectionRangeMaxValue = MapData.Keys.Count - 1;
            SelectedDataIndex = SelectionRangeMaxValue;

            IsBusy = false;
        }

        private async Task LoadMapData()
        {
            var query = new GetAllCountriesDataGroupedByDateQuery();
            MapData = await _mediator.Send(query);
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
                        Value = item.HashratePerc
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

                    shape.Value = dataItem.HashratePerc;
                }
            }

            MaxDataValue = data.Max(i => i.HashratePerc);
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
                SelectedDate = MapData.Keys.ToArray()[value];

                SetValuesForSelectedIndex();
                this.RaisePropertyChanged(nameof(SelectedDataIndex));
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => this.RaiseAndSetIfChanged(ref _selectedDate, value);
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
    }
}
