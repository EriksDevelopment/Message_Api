using System.Security.Claims;
using Message_Api.Core.Interfaces;
using Message_Api.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Message_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public readonly ILogger<MessageController> _logger;
        public MessageController(IMessageService messageService, ILogger<MessageController> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        [Authorize(Roles = "User")]
        [HttpGet("recieved-messages")]
        public async Task<ActionResult<ViewRecievedMessagesResponseDto>> GetRecievedMessages()
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _messageService.GetRecievedMessagesAsync(user);

                _logger.LogInformation("All recieved messages retrieved successfully.");
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while retrievend recieved messages.");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost("send-message")]
        public async Task<ActionResult<SendMessageResponseDto>> SendMessage(SendMessageRequestDto dto)
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _messageService.SendMessageAsync(user, dto.ToUsername, dto.Content);

                _logger.LogInformation("Message sent successfull");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while sending message.");
                return StatusCode(500, "Something went wrong.");
            }
        }
    }
}