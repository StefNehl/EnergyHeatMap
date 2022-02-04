using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Themes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Client.ViewModels
{
    public class ChartViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;

        private IEnumerable<ISeries> _seriesCollection;
        private DateTime _startDate;
        private DateTime _endDate;

        private readonly LvcColor _mainColor;


        public ChartViewModel()
        {
            _mediator = App.IoC.Services.GetService<IMediator>();
            var color = App.IoC.Services.GetService<IAppColorService>().MainColor;
            _mainColor = new LvcColor(color.R, color.G, color.B);
        }

        public async Task LoadChartData()
        {
            var query = new GetFilteredCryptoCoinStatesByTypeQuery(new string[1]{ "Btc" }, new string[1] {"Hashrate"}, StartDate, EndDate);
            var data = await _mediator.Send(query);
            CryptoCoinStates = data.ToArray();
        }

        public async Task SetChartValues()
        {
            var series = new ISeries[CryptoCoinStates.Count()];
            for (int i = 0; i<series.Length; i++)
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

                var newLineSeries = new LineSeries<FinancialPoint>()
                {
                    Values = valuesToAdd,
                    LineSmoothness = 0,
                    GeometrySize = 0,
                    GeometryStroke = new SolidColorPaint(SKColor.Empty, 0),
                    Stroke = new SolidColorPaint(new SKColor(_mainColor.R, _mainColor.G, _mainColor.B)) { StrokeThickness = 1 }
                };

                series[i] = newLineSeries;
            }

            SeriesCollection = series;
        }

        public async Task LoadAndSetChartData()
        {
            await LoadChartData();
            await SetChartValues();
        }

        public ICryptoStateData[] CryptoCoinStates { get; set; }

        public IEnumerable<ISeries> SeriesCollection 
        {
            get => _seriesCollection;
            set => this.RaiseAndSetIfChanged(ref _seriesCollection, value);
        }

        public DateTime StartDate
        {
            get => _startDate;
            set => this.RaiseAndSetIfChanged(ref _startDate, value);
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => this.RaiseAndSetIfChanged(ref _endDate, value);
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
    }
}
