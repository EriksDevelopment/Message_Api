using System.Security.Claims;
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

        [Authorize(Roles = "User")]
        [HttpGet("view-friends")]
        public async Task<ActionResult<ViewFriendsResponseDto>> ViewFriends()
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _userService.ViewFriendsAsync(user);

                _logger.LogInformation("All friends retrieved successfully.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while retireving all friend.");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete("delete-account")]
        public async Task<ActionResult<UserDeleteResponseDto>> DeleteUser(UserDeleteRequestDto dto)
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _userService.DeleteUserAsync(dto, user);

                _logger.LogInformation("User deleted successfully.");
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while deleting account.");
                return StatusCode(500, "Something went wrong.");
            }
        }
    }
}