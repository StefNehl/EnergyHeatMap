using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Infrastructure.Queries.Analysis;
using EnergyHeatMap.Infrastructure.Queries.Chart;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyHeatMap.Client.ViewModels
{
    public class AnalysisViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;

        private double _correlationCoefficent;
        private readonly ObservableCollection<ObservablePoint> _observableValues;

        private IEnumerable<IAnalysisType> _analysisTypes;
        private DateTimeOffset _startDate;
        private DateTimeOffset _endDate;
        private IAnalysisType _selectedAnalysisType;

        private bool _isFilterBusy;
        private bool _isChartBusy;
       
        public AnalysisViewModel()
        {
            _mediator = App.IoC.Services.GetService<IMediator>();
            _observableValues = new ObservableCollection<ObservablePoint>();
            Series = new ObservableCollection<ISeries>
            {
                new ScatterSeries<ObservablePoint>{ Values = _observableValues, GeometrySize = 2}
            };
            StartDate = new DateTime(2010, 1, 1);
            EndDate = DateTime.Now;

            OnLoadDataCommand = ReactiveCommand.Create(async () =>
            {
                await LoadHashrateValueCoefData();
            });
        }

        public IReactiveCommand OnLoadDataCommand { get; }

        public IEnumerable<IAnalysisType> AnalysisTypes 
        {
            get => _analysisTypes;
            set => this.RaiseAndSetIfChanged(ref _analysisTypes, value);
        }

        public double CorrelationCoefficent
        {
            get => _correlationCoefficent;
            set => this.RaiseAndSetIfChanged(ref _correlationCoefficent, value);
        }

        public ObservableCollection<ISeries> Series { get; set; }

        public DateTimeOffset StartDate
        {
            get => _startDate;
            set => this.RaiseAndSetIfChanged(ref _startDate, value);
        }

        public DateTimeOffset EndDate
        {
            get => _endDate;
            set => this.RaiseAndSetIfChanged(ref _endDate, value);
        }

        public IAnalysisType SelectedAnalysisType
        {
            get => _selectedAnalysisType;
            set => this.RaiseAndSetIfChanged(ref _selectedAnalysisType, value);
        }

        public bool IsFilterBusy
        {
            get => _isFilterBusy;
            set => this.RaiseAndSetIfChanged(ref _isFilterBusy, value);
        }

        public bool IsChartBusy
        {
            get => _isChartBusy;
            set => this.RaiseAndSetIfChanged(ref _isChartBusy, value);
        }

        public async Task LoadFilterValues()
        {
            IsFilterBusy = true;
            var analysisTypesQuery = new GetAnalysisTypesQuery();
            AnalysisTypes = await _mediator.Send(analysisTypesQuery);
            if(AnalysisTypes != null && AnalysisTypes.Any())
                SelectedAnalysisType = AnalysisTypes.First();
            IsFilterBusy = false;
        }

        public async Task LoadHashrateValueCoefData()
        {
            IsChartBusy = true;
            var corCoeQuery = new GetAnalysisValueQuery(_startDate.DateTime, _endDate.DateTime, _selectedAnalysisType);
            CorrelationCoefficent = await _mediator.Send(corCoeQuery);

            var dataQuery = new GetAnalysisDataQuery(_startDate.DateTime, _endDate.DateTime, _selectedAnalysisType);
            var coinState = await _mediator.Send(dataQuery);

            IsChartBusy = false;

            var hashRate = coinState.Select(y => y.Item1).ToArray();
            var values = coinState.Select(x => x.Item2).ToArray();
            _observableValues.Clear();
            for (int i = 0; i < values.Length; i++)
            {
                _observableValues.Add(new(values[i], hashRate[i]));
            }
        }


    }
}
