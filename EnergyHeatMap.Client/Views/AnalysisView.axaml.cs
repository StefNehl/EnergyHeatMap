using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EnergyHeatMap.Client.Views
{
    public partial class AnalysisView : UserControl
    {
        public AnalysisView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
