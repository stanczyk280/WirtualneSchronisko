using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser<int>> _userManager;

        public UserRepository(UserManager<IdentityUser<int>> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityUser<int>> GetUserByIdAsync(int userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<IEnumerable<IdentityUser<int>>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IdentityUser<int>> CreateUserAsync(IdentityUser<int> user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return user;
            }

            return null;
        }

        public async Task<IdentityUser<int>> UpdateUserAsync(IdentityUser<int> user, string newPassword)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id.ToString());

            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;

                var result = await _userManager.UpdateAsync(existingUser);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        var passwordChangeResult = await _userManager.ChangePasswordAsync(existingUser, null, newPassword);

                        if (passwordChangeResult.Succeeded)
                        {
                            return existingUser;
                        }
                        else
                        {
                            return null;
                        }
                    }

                    return existingUser;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
    }
}