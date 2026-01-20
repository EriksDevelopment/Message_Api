using Message_Api.Core.Interfaces;
using Message_Api.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Message_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterResponseDto>> AddUser(UserRegisterRequestDto dto)
        {
            try
            {
                var result = await _userService.AddUserAsync(dto);

                _logger.LogInformation("User registered Successfull.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while registering.");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDto>> LoginUser(UserLoginRequestDto dto)
        {
            try
            {
                var result = await _userService.LoginUserAsync(dto);

                _logger.LogInformation("Login successfull.");
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while logging in.");
                return StatusCode(500, "Something went wrong.");
            }

        }
    }
}