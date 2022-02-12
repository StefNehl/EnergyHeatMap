using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Enums
{
    public enum AnalysisTypes
    {
        None = 0,
        CorrelationHashrateValue = 1,
        CorrelationEnergyHashrate = 2,
    }

    public class AnalysisTypesExtansion
    {
        public static List<IAnalysisType> GetValues()
        {
            var result = new List<IAnalysisType>();

            result.Add(new AnalysisType(AnalysisTypes.None, "None"));
            result.Add(new AnalysisType(AnalysisTypes.CorrelationHashrateValue, "Correlation of Hashrate and Value"));
            result.Add(new AnalysisType(AnalysisTypes.CorrelationEnergyHashrate, "Correlation of Energy Consumption and Hashrate"));

            return result;
        }

        public static string GetPrettyString(AnalysisTypes cryptoValueType)
        {
            var value = GetValues().FirstOrDefault(i => i.Type == cryptoValueType);
            if (value == null)
                return string.Empty;

            return value.Name;
        }
    }

    public record AnalysisType : IAnalysisType
    {
        public AnalysisType(AnalysisTypes type, string name)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; }

        public AnalysisTypes Type { get; }
    }

}
