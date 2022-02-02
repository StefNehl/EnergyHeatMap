using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace EnergyHeatMap.Client.Controls
{
    public partial class LegendControl : UserControl
    {
        private Rectangle _heatIndicatorRectangle;
        private TextBlock _maxValueLabel; 

        public LegendControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _heatIndicatorRectangle = this.FindControl<Rectangle>("heatIndicator");
            _maxValueLabel = this.FindControl<TextBlock>("maxValueLabel");

        }

        public static readonly StyledProperty<SolidColorBrush> MaxBrushProperty =
            AvaloniaProperty.Register<Border, SolidColorBrush>(nameof(Background), defaultBindingMode:Avalonia.Data.BindingMode.OneWay);

        public static readonly DirectProperty<LegendControl, double> MaxValueProperty =
            AvaloniaProperty.RegisterDirect<LegendControl, double>(nameof(TextBlock), o => o.MaxValue, (o, v) => o.MaxValue = v);

        public SolidColorBrush MaxBrush
        {
            get { return GetValue(MaxBrushProperty); }
            set 
            { 
                _heatIndicatorRectangle.Fill = value;
                SetValue(MaxBrushProperty, value); 
            }
        }

        private double _maxValue; 

        public double MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValueLabel.Text = (value/100).ToString("P2");
                SetAndRaise(MaxValueProperty, ref _maxValue, value);
            }
        }
    }
}
