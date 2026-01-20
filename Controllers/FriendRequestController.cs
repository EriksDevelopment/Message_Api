using System.Security.Claims;
using Message_Api.Core.Interfaces;
using Message_Api.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Message_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendRequestController : ControllerBase
    {
        private readonly IFriendRequestService _friendRequestService;
        private readonly ILogger<FriendRequestController> _logger;
        public FriendRequestController(IFriendRequestService friendRequestService, ILogger<FriendRequestController> logger)
        {
            _friendRequestService = friendRequestService;
            _logger = logger;
        }

        [Authorize(Roles = "User")]
        [HttpPost("send-friend-request")]
        public async Task<ActionResult<SendFriendRequestResponseDto>> SendFriendRequest([FromBody] SendFriendRequestRequestDto dto)
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _friendRequestService.SendFriendRequestAsync(user, dto.FriendTag);

                _logger.LogInformation("Successfully sent friend request.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while sending friend request.");
                return StatusCode(500, "Something went wrong");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost("{requestTag}/accept")]
        public async Task<IActionResult> AcceptFriendRequest(string requestTag)
        {
            try
            {
                await _friendRequestService.AcceptFriendRequestAsync(requestTag);

                _logger.LogInformation("Successfully accepted friend request.");
                return Ok(new { Message = "Friend request accepted." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while accepting friend request.");
                return StatusCode(500, "Something went wrong");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost("{requestTag}/decline")]
        public async Task<IActionResult> DeclineFriendRequest(string requestTag)
        {
            try
            {
                await _friendRequestService.DeclineFriendRequestAsync(requestTag);

                _logger.LogInformation("Successfully declined friend request.");
                return Ok(new { Message = "Friend request declined." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while declining friend request.");
                return StatusCode(500, "Something went wrong");
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingRequests()
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var pending = await _friendRequestService.GetPendingRequestsAsync(userId);

                _logger.LogInformation("Successfully retireved all pending friend requests.");
                return Ok(pending);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while retrieving all pending friend requests.");
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}