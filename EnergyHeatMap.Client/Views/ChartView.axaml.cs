using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EnergyHeatMap.Client.Views
{
    public partial class ChartView : UserControl
    {
        public ChartView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
