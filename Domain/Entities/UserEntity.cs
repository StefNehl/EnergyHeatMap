using EnergyHeatMap.Contracts.Entities;
using System.ComponentModel.DataAnnotations;

namespace EnergyHeatMap.Domain.Entities
{
    public class UserEntity : IUserEntity
    {
        public Guid Id { get; set; } = Guid.Empty;

        [Required]
        public string Username { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string Role { get; set; } = Models.Role.None;
    }
}