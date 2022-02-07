using EnergyHeatMap.Infrastructure.Queries.Analysis;
using EnergyHeatMap.Infrastructure.Queries.Chart;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyHeatMap.Client.ViewModels
{
    public class AnalysisViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;

        private double _correlationCoefficent;
        private double[,] _chartData = new double[5, 5];

        public AnalysisViewModel()
        {
            _mediator = App.IoC.Services.GetService<IMediator>();
        }

        public double CorrelationCoefficent
        {
            get => _correlationCoefficent;
            set => this.RaiseAndSetIfChanged(ref _correlationCoefficent, value);
        }

        public double[,] ChartData
        {
            get => _chartData;
            set => this.RaiseAndSetIfChanged(ref _chartData, value);
        }

        public async Task LoadHashrateValueCoefData()
        {
            var corCoeQuery = new GetCorrelationCoefficentForHashrateAndValueQuery();
            CorrelationCoefficent = await _mediator.Send(corCoeQuery);

            //var coinValueQuery = new GetAllCryptoCoinStatesQuery();
            //var coinState = await _mediator.Send(coinValueQuery);

            //var values = coinState.Select(x => x.Value).ToArray();
            //var hashRate = coinState.Select(y => y.Hashrate).ToArray();

            //ChartData = new double[values.Length, hashRate.Length];

            //for(int i = 0; i < values.Length; i++)
            //{
            //    for(int j = 0; j < hashRate.Length; j++)
            //    {
            //        ChartData[i, j] = values[]
            //    }
            //}
        }
    }
}
