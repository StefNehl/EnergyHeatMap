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

            await MapViewModel.LoadAndSetMapData();
        }


        public string Greeting => "Energy Heat Map";

        public MapViewModel MapViewModel { get; set; }
        public ChartViewModel ChartViewModel { get; set; }
    }
}
