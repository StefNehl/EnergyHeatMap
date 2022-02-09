using EnergyHeatMap.Contracts.Repositories;
using System.Drawing;

namespace EnergyHeatMap.Infrastructure.Services
{
    public class AppColorService : IAppColorService
    {
        public AppColorService()
        {
            MainColor = ColorTranslator.FromHtml("#0288d1");

            //https://hexcolor.co/material-design-colors
            SeriesColors = new Color[10] 
            { 
                MainColor, 
                ColorTranslator.FromHtml("#d50000"),
                ColorTranslator.FromHtml("#6200ea"),
                ColorTranslator.FromHtml("#c51162"),
                ColorTranslator.FromHtml("#304ffe"),
                ColorTranslator.FromHtml("#2962ff"),
                ColorTranslator.FromHtml("#aa00ff"),
                ColorTranslator.FromHtml("#004d40"),
                ColorTranslator.FromHtml("#ff6f00"),
                ColorTranslator.FromHtml("#dd2c00"),
            };
            AlphaValueForOpacity = 30;
        }
        public Color MainColor { get; set; } 
        public Color[] SeriesColors { get; set; }
        public int AlphaValueForOpacity { get; set; }
    }
}
