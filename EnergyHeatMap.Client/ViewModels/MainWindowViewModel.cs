using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public async Task InitApplication()
        {
            MapViewModel = new MapViewModel();
            ChartViewModel = new ChartViewModel();
            AnalysisViewModel = new AnalysisViewModel();

            await MapViewModel.LoadAndSetMapData();
            await ChartViewModel.LoadAndSetChartData();
            await AnalysisViewModel.LoadHashrateValueCoefData();
        }


        public string Greeting => "Energy Heat Map";

        public MapViewModel MapViewModel { get; set; }
        public ChartViewModel ChartViewModel { get; set; }
        public AnalysisViewModel AnalysisViewModel { get; set; }
        
    }
}
