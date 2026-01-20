using System.Security.Claims;
using Message_Api.Core.Interfaces;
using Message_Api.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Message_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;
        private readonly ILogger<FriendshipController> _logger;
        public FriendshipController(IFriendshipService friendshipService, ILogger<FriendshipController> logger)
        {
            _friendshipService = friendshipService;
            _logger = logger;
        }

        [Authorize(Roles = "User")]
        [HttpPost("add-friend")]
        public async Task<ActionResult<AddFriendResponseDto>> AddFriend([FromBody] AddFriendRequestDto dto)
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _friendshipService.AddFriendAsync(user, dto.FriendTag);

                _logger.LogInformation("Friend added successfully.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while adding friend.");
                return StatusCode(500, "Something went wrong.");
            }
        }
    }
}