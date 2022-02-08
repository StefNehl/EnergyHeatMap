using EnergyHeatMap.Infrastructure.Queries.Analysis;
using EnergyHeatMap.Infrastructure.Queries.Chart;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
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
        private double[,] _chartData = new double[5, 5];
        private readonly ObservableCollection<ObservablePoint> _observableValues; 
       
        public AnalysisViewModel()
        {
            _mediator = App.IoC.Services.GetService<IMediator>();
            _observableValues = new ObservableCollection<ObservablePoint>();
            Series = new ObservableCollection<ISeries>
            {
                new ScatterSeries<ObservablePoint>{ Values = _observableValues, GeometrySize = 2}
            };
        }

        public double CorrelationCoefficent
        {
            get => _correlationCoefficent;
            set => this.RaiseAndSetIfChanged(ref _correlationCoefficent, value);
        }

        public async Task LoadHashrateValueCoefData()
        {
            var corCoeQuery = new GetCorrelationCoefficentForHashrateAndValueQuery();
            CorrelationCoefficent = await _mediator.Send(corCoeQuery);

            var coinValueQuery = new GetAllCryptoCoinStatesQuery();
            var coinState = await _mediator.Send(coinValueQuery);

            var values = coinState.Select(x => x.Value).ToArray();
            var hashRate = coinState.Select(y => y.Hashrate).ToArray();


            for (int i = 0; i < values.Length; i++)
            {
                _observableValues.Add(new(values[i], hashRate[i]));
            }
        }

        public ObservableCollection<ISeries> Series { get; set; }
    }
}
