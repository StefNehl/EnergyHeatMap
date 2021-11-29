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
    public static class UserExtensions
    {
        public static IUser ToUserModel(this IUserEntity userEntity)
        {
            return new User
            {
                Id = userEntity.Id,
                CreatedOn = userEntity.CreatedOn,
                Username = userEntity.Username,
                Role = userEntity.Role
            };
        }

        public static IUserEntity ToUserEnity(this IUser user)
        {
            return new UserEntity
            {
                Id = user.Id,
                CreatedOn = user.CreatedOn,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
