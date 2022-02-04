using EnergyHeatMap.Contracts.Repositories;
using System.Drawing;

namespace EnergyHeatMap.Infrastructure.Services
{
    public class AppColorService : IAppColorService
    {
        public Color MainColor { get; set; } = ColorTranslator.FromHtml("#0288d1");
    }
}
