using Microsoft.AspNetCore.Identity;

namespace API.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityUser<int>> GetUserByIdAsync(int userId);

        Task<IEnumerable<IdentityUser<int>>> GetUsersAsync();

        Task<IdentityUser<int>> CreateUserAsync(IdentityUser<int> user, string password);

        Task<IdentityUser<int>> UpdateUserAsync(IdentityUser<int> user, string newPassword);

        Task DeleteUserAsync(int userId);
    }
}