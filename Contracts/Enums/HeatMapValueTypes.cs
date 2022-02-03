using EnergyHeatMap.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Domain.Enums
{
    public enum HeatMapValueTypes
    {
        HashratePerc = 0,
        EnergyConsuptionPerc = 1
    }

    public static class HeatMapValueTypesExtensions
    {
        public static List<HeatMapValueType> GetValues()
        {
            var result = new List<HeatMapValueType>();

            result.Add(new HeatMapValueType(HeatMapValueTypes.HashratePerc, "Hashrate Percentage"));
            result.Add(new HeatMapValueType(HeatMapValueTypes.EnergyConsuptionPerc, "Energy Consumption Percentage"));

            return result;
        }

        public static string GetPrettyString(HeatMapValueTypes heatmapValueType)
        {
            var value = GetValues().FirstOrDefault(i => i.Type == heatmapValueType);
            if (value == null)
                return string.Empty;

            return value.Name;
        }
    }

    public class HeatMapValueType : IHeatMapValueType
    {
        public HeatMapValueType(HeatMapValueTypes type, string name)
        {
            Type = type;
            Name = name;
        }

        public HeatMapValueTypes Type { get; }
        public string Name { get; }
    }
}
