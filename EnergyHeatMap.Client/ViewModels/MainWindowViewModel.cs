using EnergyHeatMap.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System.Threading;
using System.Threading.Tasks;

namespace EnergyHeatMap.Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private bool isBusy;
        public async Task InitApplication(CancellationToken ct = default)
        {
            IsBusy = true;
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
            IsBusy = false;
        }


        public string Greeting => "Energy Heat Map";

        public MapViewModel MapViewModel { get; set; }
        public ChartViewModel ChartViewModel { get; set; }
        public AnalysisViewModel AnalysisViewModel { get; set; }
        
        public bool IsBusy
        {
            get => isBusy;
            set => this.RaiseAndSetIfChanged(ref isBusy, value);
        }
    }
}
