using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Contracts.Repositories
{
    public interface IUsersService
    {
        Task<IUser> CreateNewUser(string name, string password);
        Task<bool> DeleteUser(Guid id);
        Task<IEnumerable<IUser>> GetAllAsync();
        Task<IUser?> GetById(Guid id);
        Task<IUser?> UpdateUser(Guid id, string name);

        Task<IUser> AuthenticateAsync(string username, string password, CancellationToken ct);
    }
}