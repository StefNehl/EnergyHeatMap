using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using EnergyHeatMap.Contracts.Entities;
using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Domain.Entities;
using EnergyHeatMap.Domain.Extensions;
using EnergyHeatMap.Domain.Models;
using EnergyHeatMap.Infrastructure.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EnergyHeatMap.Contracts.Enums;

namespace EnergyHeatMap.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly string _secret = string.Empty;
        public UsersService(IOptionsMonitor<SecuritySettings> optionsMonitor)
        {
            var securitySettings = optionsMonitor.CurrentValue;

            if(securitySettings == null || securitySettings.Secret == null || securitySettings.AdminPassword == null || securitySettings.AdminUsername == null)
                throw new ArgumentNullException(nameof(securitySettings));

            if (securitySettings?.Secret != null)
                _secret = securitySettings.Secret;

            if (securitySettings?.AdminPassword == null || securitySettings?.AdminUsername == null)
                return;

            var adminUser = new UserEntity()
            {
                Password = Util.ComputeHash(securitySettings.AdminPassword, _secret),
                Username = securitySettings.AdminUsername, 
                Role = Role.Admin
            };

            Users.Add(adminUser);
        }

        private List<IUserEntity> Users { get; set; } = new List<IUserEntity>();


        public async Task<IEnumerable<IUser>> GetAllAsync()
        {
            await Task.Delay(5);

            if (Users.Count == 0)
                return new List<IUser>();
            return Users.Select(i => i.ToUserModel());
        }

        public async Task<IUser?> GetById(Guid id)
        {
            await Task.Delay(5);
            return Users.FirstOrDefault(i => i.Id == id)?.ToUserModel();
        }

        public async Task<IUser> CreateNewUser(string name, string password)
        {
            
            var newId = GetNewId();
            var newUserEntity = new UserEntity()
            {
                Id = newId,
                Username = name,
                Password = Util.ComputeHash(password, _secret)
            };

            Users.Add(newUserEntity);
            await Task.Yield();
            return newUserEntity.ToUserModel();
        }

        public async Task<IUser?> UpdateUser(Guid id, string username)
        {
            var userToUpdate = Users.FirstOrDefault(i => i.Id == id)?
                .ToUserModel();

            if (userToUpdate == null)
                return null;

            userToUpdate.Username = username;
            await Task.Yield();
            return userToUpdate;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var userToDelete = Users.FirstOrDefault(i => i.Id == id);

            if (userToDelete == null)
                return false;

            Users.Remove(userToDelete);
            await Task.Yield();
            return true;
        }

        public async Task<IUser> AuthenticateAsync(string username, string password, CancellationToken ct)
        {
            try
            {
                var userEntity = Users.SingleOrDefault(x =>
                    x.Username == username
                    && x.Password == Util.ComputeHash(password, _secret));

                //ToDo Remove test user
                if (userEntity == null)
                    userEntity = Users.SingleOrDefault(u =>
                        u.Username == username &&
                        u.Password == Util.ComputeHash(password, _secret));

                // return null if user not found
                if (userEntity == null)
                    return null;

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secret);

                var claims = new Claim[] 
                { 
                    new (ClaimTypes.Name, userEntity.Id.ToString()),
                    new (ClaimTypes.Role, userEntity.Role)
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var user = userEntity.ToUserModel();
                user.Token = tokenHandler.WriteToken(token);
                await Task.Yield();

                return user;
            }
            catch (Exception ex)
            {
                //_logger.Log(LogLevel.Debug, ex.Message);
                throw;
            }
        }

        private static Guid GetNewId() => Guid.NewGuid();
    }

}
