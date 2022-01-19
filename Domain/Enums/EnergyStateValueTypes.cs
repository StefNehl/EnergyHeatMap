﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Domain.Enums
{
    public enum EnergyStateValueTypes
    {
        Population = 0,
        PrimaryEnergyConsumption = 1,
        ElectricityGeneration = 2
    }

    public static class EnergyStateValueTypesExtensions
    {
        public static List<EnergyStateValueType>  GetValues()
        {
            var result = new List<EnergyStateValueType>();

            result.Add(new EnergyStateValueType(EnergyStateValueTypes.Population.ToString(), EnergyStateValueTypes.Population.ToString()));
            result.Add(new EnergyStateValueType(EnergyStateValueTypes.PrimaryEnergyConsumption.ToString(), "Primary Energy Consumption"));
            result.Add(new EnergyStateValueType(EnergyStateValueTypes.ElectricityGeneration.ToString(), "Electricity Generation"));

            return result;
        }

    }

    public class EnergyStateValueType
    {
        public EnergyStateValueType(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public string Type { get;}
        public string Name { get;}
    }
}
