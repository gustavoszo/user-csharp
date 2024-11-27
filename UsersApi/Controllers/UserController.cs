using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Dtos;
using UsersApi.Models;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {

        private IMapper _iMapper;
        private UserManager<User> _userManager;

        public UserController(IMapper iMapper, UserManager<User> userManager)
        {
            _iMapper = iMapper;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> create(CreateUserDto userDto)
        {
            User user = _iMapper.Map<User>(userDto);

            IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Errors.Any())
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }

            return Ok(new { Message = "Usuário cadastrado" }); 
        }

    }
}
