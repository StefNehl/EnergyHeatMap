using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Domain.Models
{
    public class User : IUser
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string Username { get; set; } = "";

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string Token { get; set; } = "";
        public string Role { get; set; } = Models.Role.None;
    }
}
