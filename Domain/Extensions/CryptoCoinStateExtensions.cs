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
            return new CryptoCoinState()
            {
                CoinName = entity.CoinName,
                DateTime = entity.DateTime,
                Difficulty = entity.Difficulty,
                Hashrate = entity.Hashrate,
                Value = entity.Value
            };
        }

        public static ICryptoCoinStateEntity ToEntity(this ICryptoCoinState model)
        {
            return new CryptoCoinStateEntity()
            {
                CoinName = model.CoinName,
                DateTime = model.DateTime,
                Difficulty = model.Difficulty,
                Hashrate = model.Hashrate,
                Value = model.Value
            };
        }
    }
}
