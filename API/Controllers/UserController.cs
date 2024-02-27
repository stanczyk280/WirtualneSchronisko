using API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound($"User with ID {userId} not found");
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            return Ok(users);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] IdentityUser<int> user, string password)
        {
            var createdUser = await _userRepository.CreateUserAsync(user, password);

            if (createdUser != null)
            {
                return CreatedAtAction(nameof(GetUser), new { userId = createdUser.Id }, createdUser);
            }

            return BadRequest("Failed to create user");
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] IdentityUser<int> user, [FromQuery] string newPassword)
        {
            try
            {
                var updatedUser = await _userRepository.UpdateUserAsync(user, newPassword);

                if (updatedUser != null)
                {
                    return Ok(updatedUser);
                }

                return BadRequest("Failed to update user");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the user: " + ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userRepository.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}