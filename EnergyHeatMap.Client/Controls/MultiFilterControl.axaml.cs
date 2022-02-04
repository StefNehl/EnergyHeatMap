using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EnergyHeatMap.Client.Controls
{
    public partial class MultiFilterControl : UserControl
    {
        public MultiFilterControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
