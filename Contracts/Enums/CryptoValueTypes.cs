using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Enums
{
    public enum CryptoValueTypes
    {
        All = 0,
        Value = 1,
        Hashrate = 2,
        //Difficulty = 3
    }

    public static class CryptoValueTypesExtensions
    {
        public static List<ICryptoValueType> GetValues()
        {
            var result = new List<ICryptoValueType>();

            result.Add(new CryptoValueType(CryptoValueTypes.Value, "Value"));
            result.Add(new CryptoValueType(CryptoValueTypes.Hashrate, "Hashrate"));

            return result;
        }

        public static string GetPrettyString(CryptoValueTypes cryptoValueType)
        {
            var value = GetValues().FirstOrDefault(i => i.Type == cryptoValueType);
            if (value == null)
                return string.Empty;

            return value.Name;
        }
    }

    public class CryptoValueType : ICryptoValueType
    {
        public CryptoValueType(CryptoValueTypes type, string name)
        {
            Type = type;
            Name = name;
        }

        public CryptoValueTypes Type { get; }
        public string Name { get; }
    }
}
