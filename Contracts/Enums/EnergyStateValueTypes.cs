
namespace EnergyHeatMap.Contracts.Enums
{
    public enum EnergyStateValueTypes
    {
        Population = 0,
        PrimaryEnergyConsumption = 1,
        ElectricityGeneration = 2,
        HashrateProductionInPercentage = 3,
        HashrateProductionInAbs = 4
    }

    public static class EnergyStateValueTypesExtensions
    {
        public static List<EnergyStateValueType>  GetValues()
        {
            var result = new List<EnergyStateValueType>();

            //result.Add(new EnergyStateValueType(EnergyStateValueTypes.Population, EnergyStateValueTypes.Population.ToString()));
            result.Add(new EnergyStateValueType(EnergyStateValueTypes.PrimaryEnergyConsumption, "Primary Energy Consumption"));
            //result.Add(new EnergyStateValueType(EnergyStateValueTypes.ElectricityGeneration, "Electricity Generation"));
            result.Add(new EnergyStateValueType(EnergyStateValueTypes.HashrateProductionInPercentage, "Hashrate Generation Percentage"));
            result.Add(new EnergyStateValueType(EnergyStateValueTypes.HashrateProductionInAbs, "Hashrate Generation Abs."));

            return result;
        }

        public static string GetPrettyString(EnergyStateValueTypes energyStateValueTypes)
        {
            var value = GetValues().FirstOrDefault(i => i.Type == energyStateValueTypes);
            if(value == null)
                return string.Empty;

            return value.Name;
        }
    }

    public class EnergyStateValueType : IEnergyStateValueType
    {
        public EnergyStateValueType(EnergyStateValueTypes type, string name)
        {
            Type = type;
            Name = name;
        }

        public EnergyStateValueTypes Type { get; }
        public string Name { get; }
    }
}
