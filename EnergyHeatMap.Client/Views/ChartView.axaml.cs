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

            var textbox = this.FindControl<TextBlock>("dateTextBlock");

            var chart = this.FindControl<CartesianChart>("chartControl");
            var tooltip = chart.Tooltip as DefaultTooltip;
            tooltip.PointerMoved += (e,a) =>
            {
                var tip = e as DefaultTooltip;
                if (tip == null)
                    return;

                if (!tip.Points.Any())
                    return;

                var value = tip.Points.FirstOrDefault().SecondaryValue;

                System.Console.WriteLine(new DateTime((long)value));
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
