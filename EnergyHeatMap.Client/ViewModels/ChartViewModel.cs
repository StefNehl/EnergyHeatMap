using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries.Analysis;
using EnergyHeatMap.Infrastructure.Queries.Chart;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyHeatMap.Client.ViewModels
{
    public class ChartViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly IAppColorService _appColorService;

        private ObservableCollection<ICryptoValueType> _selectedCryptoValueTypes = new ();
        private ObservableCollection<string> _selectedCountries = new ();
        private ObservableCollection<IEnergyStateValueType> _selectedEnergyValueType = new ();

        private IEnumerable<ISeries> _seriesCollection;
        private DateTimeOffset _startDate;
        private DateTimeOffset _endDate;

        public ChartViewModel()
        {
            _mediator = App.IoC.Services.GetService<IMediator>();
            _appColorService = App.IoC.Services.GetService<IAppColorService>();

            SelectedCryptoValueTypes.CollectionChanged += RefreshAfterFilterSelection;
            SelectedCountries.CollectionChanged += RefreshAfterFilterSelection;
            SelectedEnergyValueType.CollectionChanged += RefreshAfterFilterSelection;

            _startDate = new DateTime(2010, 1, 1);
            _endDate = DateTime.Now;

            OnLoadDataCommand = ReactiveCommand.Create(async () =>
            {
                await LoadAndSetChartData();
            });
        }
        public IReactiveCommand OnLoadDataCommand { get; }

        public ICryptoStateData[] CryptoCoinStates { get; set; }

        public IEnergyStateData[] EnergyStates { get; set; }

        public string[] Countries { get; set; }

        public ICryptoValueType[] CryptoValueTypes { get; set; }

        public IEnergyStateValueType[] EnergyStateValueTypes { get; set; }

        public ObservableCollection<ICryptoValueType> SelectedCryptoValueTypes
        {
            get => _selectedCryptoValueTypes;
            set => this.RaiseAndSetIfChanged(ref _selectedCryptoValueTypes, value); 
        }

        public ObservableCollection<string> SelectedCountries
        {
            get => _selectedCountries;
            set => this.RaiseAndSetIfChanged(ref _selectedCountries, value);
        }

        public ObservableCollection<IEnergyStateValueType> SelectedEnergyValueType
        {
            get => _selectedEnergyValueType;
            set => this.RaiseAndSetIfChanged(ref _selectedEnergyValueType, value);

        }

        public IEnumerable<ISeries> SeriesCollection
        {
            get => _seriesCollection;
            set => this.RaiseAndSetIfChanged(ref _seriesCollection, value);
        }

        public DateTimeOffset StartDate
        {
            get => _startDate;
            set 
            {
                this.RaiseAndSetIfChanged(ref _startDate, value);
                Task.Run(async () =>
                {
                    await LoadAndSetChartData();
                });
            }
        }

        public DateTimeOffset EndDate
        {
            get => _endDate;
            set
            {
                this.RaiseAndSetIfChanged(ref _endDate, value);
                Task.Run(async () =>
                {
                    await LoadAndSetChartData();
                });
            }
        }


        public Axis[] XAxes { get; set; } = {
            new()
            {
                LabelsRotation = 15,
                Labeler = value => new DateTime((long)value).ToString("dd/MM/yyyy"),
                // set the unit width of the axis to "days"
                // since our X axis is of type date time and 
                // the interval between our points is in days
                UnitWidth = TimeSpan.FromDays(1).Ticks
            }
        };

        public async Task LoadAndSetChartData()
        {
            await LoadChartData();
            await SetChartValues();
        }

        public async void RefreshAfterFilterSelection(object? s, EventArgs e)
        {
            await LoadChartData();
            await SetChartValues();
        }

        public async Task LoadAndSetFilterValues()
        {
            var typesQuery = new GetCryptoValueTypesQuery();
            CryptoValueTypes = (await _mediator.Send(typesQuery)).ToArray();

            if(CryptoValueTypes != null && CryptoValueTypes.Length > 1)
                SelectedCryptoValueTypes.Add(CryptoValueTypes[1]);

            var countriesQuery = new GetCountriesQuery();
            Countries = (await _mediator.Send(countriesQuery)).ToArray();

            if (Countries != null && Countries.Length != 0)
                SelectedCountries.Add(Countries[0]);

            var energyValueTypesQuery = new GetEnergyStateValueTypesQuery();
            EnergyStateValueTypes = (await _mediator.Send(energyValueTypesQuery)).ToArray();

            if (EnergyStateValueTypes != null && EnergyStateValueTypes.Length > 4)
                SelectedEnergyValueType.Add(EnergyStateValueTypes[4]);
        }

        public async Task LoadChartData()
        {
            var types = SelectedCryptoValueTypes.Select(x => x.Type.ToString()).ToArray();
            var statesQuery = new GetFilteredCryptoCoinStatesByTypeQuery(new string[1] { "Btc" }, types, StartDate.DateTime, EndDate.DateTime);
            CryptoCoinStates = (await _mediator.Send(statesQuery)).ToArray();

            var energyTypes = SelectedEnergyValueType.Select(x => x.Type.ToString()).ToArray();
            var selectedCountries = SelectedCountries.ToArray();
            var energyStatesQuery = new GetFilteredEnergyStateDataQuery(selectedCountries, energyTypes, StartDate.DateTime, EndDate.DateTime);
            EnergyStates = (await _mediator.Send(energyStatesQuery)).ToArray();
        }

        public async Task SetChartValues()
        {
            var series = new ISeries[CryptoCoinStates.Length + EnergyStates.Length];
            for (int i = 0; i< CryptoCoinStates.Length; i++)
            {
                var state = CryptoCoinStates[i];
                var valuesToAdd = new ObservableCollection<FinancialPoint>();

                if (state.Values == null)
                    continue;

                foreach (var value in state.Values)
                {
                    var newItemPrice = new FinancialPoint()
                    {
                        Date = value.DateTime,
                        High = value.Value
                    };
                    valuesToAdd.Add(newItemPrice);
                }

                var lineColor = _appColorService.SeriesColors[i];
                var fillColor = Color.FromArgb(_appColorService.AlphaValueForOpacity, lineColor);

                var newLineSeries = new LineSeries<FinancialPoint>()
                {
                    Name = state.Longname,
                    Values = valuesToAdd,
                    TooltipLabelFormatter = (x) => { return $"{x.Context.Series.Name} {x.PrimaryValue.ToString("N2")}"; },
                    DataLabelsFormatter = (x) => { return x.PrimaryValue.ToString("N2"); },
                    LineSmoothness = 0,
                    GeometrySize = 0,
                    GeometryStroke = new SolidColorPaint(new SKColor(lineColor.R, lineColor.G, lineColor.B, lineColor.A), 0),
                    GeometryFill = new SolidColorPaint(new SKColor(fillColor.R, fillColor.G, fillColor.B, fillColor.A)),
                    Stroke = new SolidColorPaint(new SKColor(lineColor.R, lineColor.G, lineColor.B, lineColor.A)) { StrokeThickness = 1 },
                    Fill = new SolidColorPaint(new SKColor(fillColor.R, fillColor.G, fillColor.B, fillColor.A))
                };

                series[i] = newLineSeries;
            }

            for (int i = CryptoCoinStates.Length; i < series.Length; i++)
            {
                var state = EnergyStates[i - CryptoCoinStates.Length];
                var valuesToAdd = new ObservableCollection<FinancialPoint>();

                if (state.Values == null)
                    continue;

                foreach (var value in state.Values)
                {
                    var newItemPrice = new FinancialPoint()
                    {
                        Date = value.DateTime,
                        High = value.Value
                    };
                    valuesToAdd.Add(newItemPrice);
                }

                var lineColor = _appColorService.SeriesColors[i];
                var fillColor = Color.FromArgb(_appColorService.AlphaValueForOpacity, lineColor);

                var newLineSeries = new LineSeries<FinancialPoint>()
                {
                    Name = state.Longname,
                    Values = valuesToAdd,
                    TooltipLabelFormatter = (x) => { return $"{x.Context.Series.Name} {x.PrimaryValue.ToString("N2")}"; },
                    DataLabelsFormatter = (x) => { return x.PrimaryValue.ToString("N2"); },
                    LineSmoothness = 0,
                    GeometrySize = 15,
                    GeometryStroke = new SolidColorPaint(new SKColor(lineColor.R, lineColor.G, lineColor.B, lineColor.A), 2),
                    GeometryFill = new SolidColorPaint(new SKColor(fillColor.R, fillColor.G, fillColor.B, fillColor.A)),
                    Stroke = new SolidColorPaint(new SKColor(lineColor.R, lineColor.G, lineColor.B, lineColor.A)) { StrokeThickness = 2 },
                    Fill = new SolidColorPaint(new SKColor(fillColor.R, fillColor.G, fillColor.B, fillColor.A))
                };

                series[i] = newLineSeries;
            }

            SeriesCollection = series;
        }
    }
}
