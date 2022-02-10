using EnergyHeatMap.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace EnergyHeatMap.Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public async Task InitApplication(CancellationToken ct = default)
        {
            var service = App.IoC.Services.GetService<ICountryEnergyStateServices>();
            if (service != null)
                await service.InitService(ct);

            MapViewModel = new MapViewModel();
            ChartViewModel = new ChartViewModel();
            AnalysisViewModel = new AnalysisViewModel();

            await MapViewModel.LoadHeatMapValueTypes();
            await MapViewModel.LoadAndSetMapData();

            await ChartViewModel.LoadAndSetFilterValues();
            await ChartViewModel.LoadAndSetChartData();

            await AnalysisViewModel.LoadFilterValues();
            await AnalysisViewModel.LoadHashrateValueCoefData();
        }


        public string Greeting => "Energy Heat Map";

        public MapViewModel MapViewModel { get; set; }
        public ChartViewModel ChartViewModel { get; set; }
        public AnalysisViewModel AnalysisViewModel { get; set; }
        
    }
}
