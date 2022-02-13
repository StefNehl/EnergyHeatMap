using Avalonia.Data.Converters;
using LiveChartsCore.SkiaSharpView.Avalonia;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Client.Converters
{
    public class PointValueToDateTimeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var toolTip = value as DefaultTooltip;
            if(toolTip == null)
                return null;

            if (!toolTip.Points.Any())
                return null;

            var dateTimeTicks = toolTip.Points.FirstOrDefault()?.SecondaryValue;
            if (dateTimeTicks == null)
                return null;

            return new DateTime((long)dateTimeTicks).ToString("d");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
