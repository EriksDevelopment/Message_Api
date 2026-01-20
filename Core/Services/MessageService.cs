using Message_Api.Core.Interfaces;
using Message_Api.Core.Services.TagGenerator;
using Message_Api.Data.Dtos;
using Message_Api.Data.Interfaces;
using Message_Api.Data.Models;

namespace Message_Api.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepo;
        private readonly IUserRepository _userRepo;
        private readonly IConversationRepository _conversationRepo;
        private readonly ConversationTagGeneratorService _conversationTagGenerator;
        public MessageService
        (
            IMessageRepository messageRepo,
            IUserRepository userRepo,
            IConversationRepository conversationRepo,
            ConversationTagGeneratorService conversationTagGenerator
        )
        {
            _messageRepo = messageRepo;
            _userRepo = userRepo;
            _conversationRepo = conversationRepo;
            _conversationTagGenerator = conversationTagGenerator;
        }

        public async Task<List<ViewRecievedMessagesResponseDto>> GetRecievedMessagesAsync(int recievedId)
        {
            var messages = await _messageRepo.GetReceivedMessagesAsync(recievedId);

            if (!messages.Any())
                throw new InvalidOperationException("No messages recieved.");

            return messages.Select(m => new ViewRecievedMessagesResponseDto
            {
                ConversationTag = m.Conversation.ConversationTag,
                Timestamp = m.Timestamp,
                FromUserName = m.Sender.User_Name,
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

            var conversation = await _conversationRepo.GetBetweenUsersAsync(senderId, reciever.Id);
            if (conversation == null)
            {
                var uniqueTag = await _conversationTagGenerator.GenerateUniqueConversationTag();

                conversation = new Conversation
                {
                    ConversationTag = uniqueTag,
                    UserAId = senderId,
                    UserBId = reciever.Id
                };

                await _conversationRepo.AddConversationAsync(conversation);
            }

            var message = new Message
            {
                ConversationId = conversation.Id,
                SenderId = senderId,
                RecieverId = reciever.Id,
                Content = content,
                Timestamp = DateTime.UtcNow,
            };

            await _messageRepo.SendMessageAsync(message);

            return new SendMessageResponseDto
            {
                Message = $"Message sent to {reciever.User_Name} at {message.Timestamp}"
            };
        }
    }
}