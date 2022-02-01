using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EnergyHeatMap.Client.Views
{
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
