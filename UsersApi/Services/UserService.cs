using Microsoft.AspNetCore.Identity;
using UsersApi.Models;

namespace UsersApi.Services
{
    public class UserService
    {
        private UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Create(User user)
        {
            return await _userManager.CreateAsync(user);
        }

    }
}
