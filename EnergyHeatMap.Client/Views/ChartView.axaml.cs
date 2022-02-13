using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Avalonia;
using System;
using System.Linq;

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
