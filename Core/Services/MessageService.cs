using Message_Api.Core.Interfaces;
using Message_Api.Data.Dtos;
using Message_Api.Data.Interfaces;

namespace Message_Api.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepo;
        public MessageService(IMessageRepository messageRepo)
        {
            _messageRepo = messageRepo;
        }

        public async Task<List<ViewRecievedMessagesResponseDto>> GetRecievedMessagesAsync(int senderId)
        {
            var messages = await _messageRepo.GetMessageBySenderIdAsync(senderId);

            if (!messages.Any())
                throw new InvalidOperationException("No messages recieved.");

            return messages.Select(m => new ViewRecievedMessagesResponseDto
            {
                Timestamp = m.Timestamp,
                UserName = m.Sender.User_Name,
                Content = m.Content
            }).ToList();
        }
    }
}