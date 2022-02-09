using EnergyHeatMap.Contracts.Entities;
using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Domain.Entities;
using EnergyHeatMap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Domain.Extensions
{
    public static class CryptoCoinStateExtensions
    {
        public static ICryptoCoinState ToModel(this ICryptoCoinStateEntity entity)
        {
            return new CryptoCoinState(entity.DateTime, entity.CoinName, entity.Value, entity.ValueUnit, entity.Hashrate, entity.HashrateUnit);
        }

        public static ICryptoCoinStateEntity ToEntity(this ICryptoCoinState model)
        {
            var newItem =  new CryptoCoinStateEntity(model.DateTime, model.CoinName, model.Value, model.ValueUnit);
            newItem.Hashrate = model.Hashrate;
            newItem.HashrateUnit = model.HashrateUnit;
            return newItem;
        }
    }
}
