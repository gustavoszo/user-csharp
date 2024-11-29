using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Dtos;
using UsersApi.Models;
using UsersApi.Services;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {

        private IMapper _iMapper;
        private UserService _userService;

        public UserController(IMapper iMapper, UserService userService)
        {
            _iMapper = iMapper;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> create(CreateUserDto userDto)
        {
            User user = _iMapper.Map<User>(userDto);

            IdentityResult result = await _userService.Create(user);
            if (result.Errors.Any())
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }

            return Ok(new { Message = "Usuário cadastrado" }); 
        }

    }
}
