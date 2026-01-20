using Message_Api.Core.Interfaces;
using Message_Api.Data.Dtos;
using Message_Api.Data.Interfaces;
using Message_Api.Data.Models;

namespace Message_Api.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepo;
        private readonly IUserRepository _userRepo;
        public MessageService(IMessageRepository messageRepo, IUserRepository userRepo)
        {
            _messageRepo = messageRepo;
            _userRepo = userRepo;
        }

        public async Task<List<ViewRecievedMessagesResponseDto>> GetRecievedMessagesAsync(int recievedId)
        {
            var messages = await _messageRepo.GetReceivedMessagesAsync(recievedId);

            if (!messages.Any())
                throw new InvalidOperationException("No messages recieved.");

            return messages.Select(m => new ViewRecievedMessagesResponseDto
            {
                Timestamp = m.Timestamp,
                UserName = m.Sender.User_Name,
                Content = m.Content
            }).ToList();
        }

        public async Task<SendMessageResponseDto> SendMessageAsync(int senderId, string friendUsername, string content)
        {
            if (string.IsNullOrWhiteSpace(friendUsername) ||
                string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Invalid, fields can't be empty.");

            var reciever = await _userRepo.GetUserByUserNameAsync(friendUsername);
            if (reciever == null)
                throw new ArgumentException("User not found.");

            var message = new Message
            {
                SenderId = senderId,
                RecieverId = reciever.Id,
                Content = content,
                Timestamp = DateTime.UtcNow
            };

            await _messageRepo.SendMessageAsync(message);

            return new SendMessageResponseDto
            {
                Message = $"Message sent to {reciever.User_Name} at {message.Timestamp}"
            };
        }
    }
}